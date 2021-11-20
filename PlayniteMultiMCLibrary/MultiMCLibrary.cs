using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using Newtonsoft.Json;
using Playnite.SDK;
using Playnite.SDK.Events;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;

namespace MultiMcLibrary
{
    public class MultiMcLibrary : LibraryPlugin
    {
        internal static readonly string AssemblyPath = Path.GetDirectoryName(typeof(MultiMcLibrary).Assembly.Location) ?? ".";
        
        private static readonly ILogger Logger = LogManager.GetLogger();

        private MultiMcLibrarySettingsViewModel SettingsViewModel { get; }
        internal MultiMcLibrarySettings Settings => SettingsViewModel.Settings;

        public override Guid Id { get; } = Guid.Parse("6ab2531e-4800-404b-a938-4421b28a9f3e");

        public override string Name => "MultiMC";

        // Implementing Client adds ability to open it via special menu in playnite.
        public override LibraryClient Client { get; }

        public string MultiMcPath => Settings.MultiMcPath;

        private readonly GameMenuItem[] _gameMenuItems;

        public MultiMcLibrary(IPlayniteAPI api) : base(api)
        {
            SettingsViewModel = new MultiMcLibrarySettingsViewModel(this);
            Properties = new LibraryPluginProperties
            {
                HasSettings = true
            };
            Client = new MultiMcLibraryClient(this);

            _gameMenuItems = new GameMenuItem[]
            {
                new()
                {
                    Description = "Refresh description",
                    MenuSection = "MultiMC",
                    Action = args1 =>
                    {
                        foreach (var game in args1.Games.Where(e => e.PluginId == Id))
                        {
                            UpdateGameDetails(game, forceDescription: true);
                        }
                        PlayniteApi.Database.Games.Update(args1.Games.Where(e => e.PluginId == Id));
                    }
                },
                new()
                {
                    Description = "Update playtime from MultiMC",
                    MenuSection = "MultiMC",
                    Action = args1 =>
                    {
                        foreach (var game in args1.Games.Where(e => e.PluginId == Id))
                        {
                            UpdateGameDetails(game, forcePlaytime: true);
                        }
                        PlayniteApi.Database.Games.Update(args1.Games.Where(e => e.PluginId == Id));
                    }
                }
            };
        }

        public override IEnumerable<GameMenuItem> GetGameMenuItems(GetGameMenuItemsArgs args)
            => args.Games.All(e => e.PluginId != Id) ? Array.Empty<GameMenuItem>() : _gameMenuItems;

        internal void SettingsChanged(MultiMcLibrarySettings before, MultiMcLibrarySettings after)
        {
            // Update game actions when MultiMC folder path changes
            if (before.MultiMcPath != after.MultiMcPath)
            {
                PlayniteApi.Database.Games.BeginBufferUpdate();
                foreach (var game in PlayniteApi.Database.Games)
                {
                    if (game.PluginId != Id)
                    {
                        continue;
                    }

                    var changed = false;
                    for (var i = 0; i < game.GameActions.Count; i++)
                    {
                        var gameAction = game.GameActions[i];
                        if (gameAction.Name == "Launch MultiMC" && gameAction.Path.EndsWith("MultiMC.exe"))
                        {
                            game.GameActions[i] = new GameAction
                            {
                                Type = GameActionType.File,
                                Path = Path.Combine(MultiMcPath, "MultiMC.exe"),
                                WorkingDir = MultiMcPath,
                                Name = "Launch MultiMC",
                            };
                            changed = true;
                        }
                    }

                    if (changed)
                    {
                        PlayniteApi.Database.Games.Update(game);
                    }
                }
                PlayniteApi.Database.Games.EndBufferUpdate();
            }
        }

        public override void OnGameStopped(OnGameStoppedEventArgs args)
        {
            if (args.Game.PluginId != Id)
            {
                return;
            }

            if (!Settings.UpdatePlaytimeOnClose && !Settings.UpdateDescriptionOnClose)
            {
                return;
            }

            UpdateGameDetails(args.Game);
            PlayniteApi.Database.Games.Update(args.Game);
        }

        public void OnClientClosed()
        {
            if (!Settings.UpdatePlaytimeOnClose && !Settings.UpdateDescriptionOnClose)
            {
                return;
            }
            
            PlayniteApi.Database.Games.BeginBufferUpdate();
            foreach (var game in PlayniteApi.Database.Games)
            {
                if (game.PluginId != Id)
                {
                    continue;
                }

                UpdateGameDetails(game);
                PlayniteApi.Database.Games.Update(game);
            }
            PlayniteApi.Database.Games.EndBufferUpdate();
        }

        internal bool TryGetInstanceInfo(string instanceFolderName, [NotNullWhen(true)] out InstanceCfg? cfg, [NotNullWhen(true)] out MultiMcPack? pack, [NotNullWhen(true)] out string? instanceFolder)
        {
            instanceFolder = Path.Combine(MultiMcPath, "instances", instanceFolderName);

            var cfgPath = Path.Combine(instanceFolder, "instance.cfg");
            var packPath = Path.Combine(instanceFolder, "mmc-pack.json");

            if (!Directory.Exists(instanceFolder) || !File.Exists(cfgPath) || !File.Exists(packPath))
            {
                cfg = default;
                pack = default;
                return false;
            }

            cfg = InstanceCfgParser.Parse(File.ReadAllLines(cfgPath));
            pack = JsonConvert.DeserializeObject<MultiMcPack>(File.ReadAllText(packPath));

            return pack != null;
        }

        internal bool TryGetInstanceInfo(Game game, [NotNullWhen(true)] out InstanceCfg? cfg, [NotNullWhen(true)] out MultiMcPack? pack, [NotNullWhen(true)] out string? instanceFolder)
        {
            return TryGetInstanceInfo(GetFolderName(game), out cfg, out pack, out instanceFolder);
        }

        public static string GetFolderName(Game game) => game.GameId;

        private void UpdateGameDetails(Game game, bool forcePlaytime = false, bool forceDescription = false)
        {
            if (!TryGetInstanceInfo(game, out var cfg, out var pack, out var instanceFolder))
            {
                return;
            }

            if (forcePlaytime || Settings.UpdatePlaytimeOnClose)
            {
                if (cfg.TotalTimePlayed != null)
                {
                    game.Playtime = cfg.TotalTimePlayed.Value;
                }

                if (cfg.LastLaunchTime != null)
                {
                    game.LastActivity = cfg.LastLaunchDateTime;
                }
            }

            if (forceDescription || Settings.UpdateDescriptionOnClose)
            {
                game.Description = DescriptionFormatter.FormatDescription(instanceFolder, cfg, pack);
            }
        }

        public override IEnumerable<GameMetadata> GetGames(LibraryGetGamesArgs args)
        {
            if (string.IsNullOrWhiteSpace(MultiMcPath))
            {
                yield break;
            }
            
            var instances = GetInstancesWithGroups();
            foreach (var instance in instances)
            {
                if (!TryGetInstanceInfo(instance.FolderName, out var cfg, out var pack, out var instanceFolder))
                {
                    continue;
                }

                yield return new GameMetadata
                {
                    Name = TokenFormatter.FormatString(Settings.InstanceNameFormat, cfg, pack),
                    GameId = instance.FolderName,
                    InstallDirectory = instanceFolder,
                    ReleaseDate = new ReleaseDate(2011, 11, 18),
                    Links = new List<Link>
                    {
                        new("Minecraft.net", "https://minecraft.net/"),
                        new("Mods on Curseforge", "https://www.curseforge.com/minecraft/mc-mods"),
                        new("Mods on Modrinth", "https://modrinth.com/mods"),
                    },
                    IsInstalled = true,
                    Playtime = cfg.TotalTimePlayed ?? 0, // seconds
                    LastActivity = cfg.LastLaunchDateTime,
                    GameActions = new List<GameAction>
                    {
                        new()
                        {
                            Type = GameActionType.File,
                            Path = Path.Combine(MultiMcPath, "MultiMC.exe"),
                            WorkingDir = MultiMcPath,
                            Name = "Launch MultiMC",
                        }
                    },
                };
            }
        }

        private readonly record struct GroupedInstance(string? Group, string FolderName);
        private IEnumerable<GroupedInstance> GetInstancesWithGroups()
        {
            if (!Directory.Exists(Path.Combine(MultiMcPath, "instances")))
            {
                return Array.Empty<GroupedInstance>();
            }

            var instanceList = new Dictionary<string, GroupedInstance>();

            var instgroupsPath = Path.Combine(MultiMcPath, "instances/instgroups.json");

            if (File.Exists(instgroupsPath))
            {
                var instgroups = JsonConvert.DeserializeObject<InstanceGroups>(File.ReadAllText(instgroupsPath))!;

                foreach (var (groupName, group) in instgroups.Groups)
                {
                    foreach (var folderName in group.Instances)
                    {
                        instanceList[folderName] = new GroupedInstance(groupName, folderName);
                    }
                }
            }
            
            foreach (var instanceFolder in Directory.EnumerateDirectories(Path.Combine(MultiMcPath, "instances")))
            {
                var instanceFolderName = Path.GetFileName(instanceFolder);
                if (!instanceList.ContainsKey(instanceFolderName))
                {
                    instanceList[instanceFolderName] = new GroupedInstance(null, instanceFolderName);
                }
            }

            return instanceList.Values;
        }

        public override LibraryMetadataProvider GetMetadataDownloader()
            => new MultiMcMetadataProvider(this);

        public override IEnumerable<PlayController> GetPlayActions(GetPlayActionsArgs args)
        {
            if (args.Game.PluginId != Id)
            {
                yield break;
            }

            yield return new AutomaticPlayController(args.Game)
            {
                Type = AutomaticPlayActionType.File,
                TrackingMode = TrackingMode.Process,
                Path = Path.Combine(MultiMcPath, "MultiMC.exe"),
                Arguments = @$"--launch ""{GetFolderName(args.Game)}""",
                WorkingDir = MultiMcPath,
                Name = "Play with MultiMC"
            };
        }

        public override ISettings GetSettings(bool firstRunSettings) => SettingsViewModel;
        public override UserControl GetSettingsView(bool firstRunSettings) => new MultiMcLibrarySettingsView();
    }
}