using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using Newtonsoft.Json;
using Playnite.SDK;
using Playnite.SDK.Events;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;

namespace MultiMcLibrary;

public abstract class BaseLauncher
{
    /// <summary>
    /// Path to the client installation
    /// </summary>
    public string InstallDirectory { get; }

    /// <summary>
    /// Display name of the client
    /// </summary>
    public abstract string Name { get; }

    public abstract string ExecutablePath { get; }
    public abstract string ConfigPath { get; }
    public abstract string ProcessName { get; }
    public abstract string IconName { get; }

    protected BaseLauncher(string installDirectory)
    {
        InstallDirectory = installDirectory;
    }
}

public class MultiMcLauncher : BaseLauncher
{
    public override string Name => "MultiMC";
    public static string RelativeExecutablePath => "MultiMC.exe";
    public override string ExecutablePath => Path.Combine(InstallDirectory, RelativeExecutablePath);
    public override string ConfigPath => Path.Combine(InstallDirectory, "multimc.cfg");
    public override string ProcessName => "MultiMC Launcher";
    public override string IconName => "icon-multimc.png";

    public MultiMcLauncher(string installDirectory) : base(installDirectory)
    {
    }
}

public class PolyMcLauncher : BaseLauncher
{
    public override string Name => "PolyMC";
    public static string RelativeExecutablePath => "polymc.exe";
    public override string ExecutablePath => Path.Combine(InstallDirectory, RelativeExecutablePath);
    public override string ConfigPath => Path.Combine(InstallDirectory, "polymc.cfg");
    public override string ProcessName => "PolyMC";
    public override string IconName => "icon-polymc.png";

    public PolyMcLauncher(string installDirectory) : base(installDirectory)
    {
    }
}

public class MultiMcLibrary : LibraryPlugin
{
    internal static readonly string AssemblyPath = Path.GetDirectoryName(typeof(MultiMcLibrary).Assembly.Location) ?? ".";
        
    private static readonly ILogger Logger = LogManager.GetLogger();

    private MultiMcLibrarySettingsViewModel SettingsViewModel { get; }
    internal MultiMcLibrarySettings Settings => SettingsViewModel.Settings;

    public override Guid Id { get; } = Guid.Parse("6ab2531e-4800-404b-a938-4421b28a9f3e");

    public override string Name => "MultiMC/PolyMC";

    // Implementing Client adds ability to open it via special menu in playnite.
    public override LibraryClient Client { get; }

    public BaseLauncher? Launcher { get; private set; }

    private readonly GameMenuItem[] _gameMenuItems;

    public MultiMcLibrary(IPlayniteAPI api) : base(api)
    {
        SettingsViewModel = new MultiMcLibrarySettingsViewModel(this);
        DetectMultiMcClient();

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

    private void DetectMultiMcClient()
    {
        var rootFolder = Settings.MultiMcPath;

        if (File.Exists(Path.Combine(rootFolder, MultiMcLauncher.RelativeExecutablePath)))
        {
            Launcher = new MultiMcLauncher(rootFolder);
        }
        else if (File.Exists(Path.Combine(rootFolder, PolyMcLauncher.RelativeExecutablePath)))
        {
            Launcher = new PolyMcLauncher(rootFolder);
        }
        else
        {
            Launcher = null;
        }
    }

    public override IEnumerable<GameMenuItem> GetGameMenuItems(GetGameMenuItemsArgs args)
        => args.Games.All(e => e.PluginId != Id) ? Array.Empty<GameMenuItem>() : _gameMenuItems;

    internal void SettingsChanged(MultiMcLibrarySettings before, MultiMcLibrarySettings after)
    {
        // Update game actions and install folder when MultiMC folder path changes
        if (before.MultiMcPath != after.MultiMcPath)
        {
            DetectMultiMcClient();

            if (Launcher == null)
            {
                DisplayLauncherError();
                return;
            }
        
            var instancesFolder = GetInstancesFolder();

            PlayniteApi.Database.Games.BeginBufferUpdate();
            foreach (var game in PlayniteApi.Database.Games)
            {
                if (game.PluginId != Id)
                {
                    continue;
                }

                var changed = false;
                var instanceFolder = Path.Combine(instancesFolder, GetFolderName(game));
                if (game.InstallDirectory != instanceFolder)
                {
                    game.InstallDirectory = instanceFolder;
                    changed = true;
                }
                    
                for (var i = 0; i < game.GameActions.Count; i++)
                {
                    var gameAction = game.GameActions[i];
                    switch (gameAction.Name)
                    {
                        case "Launch MultiMC":
                            game.GameActions[i] = new GameAction
                            {
                                Type = GameActionType.File,
                                Path = Launcher.ExecutablePath,
                                WorkingDir = Launcher.InstallDirectory,
                                Name = $"Launch {Launcher.Name}",
                            };
                            changed = true;
                            break;
                        case "Open Minecraft Folder":
                            game.GameActions[i] = new GameAction
                            {
                                Type = GameActionType.File,
                                Path = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "explorer.exe"),
                                Arguments = $@"""{Path.Combine(instanceFolder, ".minecraft")}""",
                                WorkingDir = Launcher.InstallDirectory,
                                Name = "Open Minecraft Folder",
                            };
                            changed = true;
                            break;
                        case "Open Mods Folder":
                            game.GameActions[i] = new GameAction
                            {
                                Type = GameActionType.File,
                                Path = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "explorer.exe"),
                                Arguments = $@"""{Path.Combine(instanceFolder, ".minecraft", "mods")}""",
                                WorkingDir = Launcher.InstallDirectory,
                                Name = "Open Mods Folder",
                            };
                            changed = true;
                            break;
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

    internal void DisplayLauncherError()
    {
        PlayniteApi.Dialogs.ShowErrorMessage(
            $"The path to your MultiMC/PolyMC installation isn't valid:\n{Settings.MultiMcPath}",
            "MultiMC Library Plugin Error"
        );
    }

    public override void OnGameStopped(OnGameStoppedEventArgs args) // Untested
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

    public void OnClientClosed() // Untested
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
        var instancesFolder = GetInstancesFolder();

        instanceFolder = Path.Combine(instancesFolder, instanceFolderName);

        var cfgPath = Path.Combine(instanceFolder, "instance.cfg");
        var packPath = Path.Combine(instanceFolder, "mmc-pack.json");

        if (!Directory.Exists(instanceFolder) || !File.Exists(cfgPath) || !File.Exists(packPath))
        {
            cfg = default;
            pack = default;
            return false;
        }

        cfg = LoadCfgFile<InstanceCfg>(cfgPath);
        pack = JsonConvert.DeserializeObject<MultiMcPack>(File.ReadAllText(packPath));

        return pack != null;
    }

    private string GetInstancesFolder()
    {
        if (Launcher == null)
        {
            throw new InvalidOperationException("Launcher should not be null; this should not happen");
        }
        
        // Currently this file is read once for every game, so that you don't need to restart Playnite if the config
        // changes. Could use something more sophisticated in the future.
        var multimcCfgPath = Launcher.ConfigPath;
        return Path.GetFullPath(Path.Combine(Launcher.InstallDirectory, LoadCfgFile<MultiMcCfg>(multimcCfgPath).InstanceDir));
    }

    private static T LoadCfgFile<T>(string cfgPath) where T : class, new()
    {
        return CfgParser.Parse<T>(File.ReadAllLines(cfgPath));
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
        // We don't show a message box here to avoid being obnoxious, instead one only is sent on plugin init and
        // settings change
        if (Launcher == null || string.IsNullOrWhiteSpace(Settings.MultiMcPath))
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
                        Path = Launcher.ExecutablePath,
                        WorkingDir = Launcher.InstallDirectory,
                        Name = "Launch MultiMC",
                    },
                    new() // Untested
                    {
                        Type = GameActionType.File,
                        Path = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "explorer.exe"),
                        Arguments = $@"""{Path.Combine(instanceFolder, ".minecraft")}""",
                        WorkingDir = Launcher.InstallDirectory,
                        Name = "Open Minecraft Folder",
                    },
                    new() // Untested
                    {
                        Type = GameActionType.File,
                        Path = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "explorer.exe"),
                        Arguments = $@"""{Path.Combine(instanceFolder, ".minecraft", "mods")}""",
                        WorkingDir = Launcher.InstallDirectory,
                        Name = "Open Mods Folder",
                    }
                },
            };
        }
    }

    private readonly record struct GroupedInstance(string? Group, string FolderName);
    private IEnumerable<GroupedInstance> GetInstancesWithGroups()
    {
        var instancesFolder = GetInstancesFolder();
        
        if (!Directory.Exists(instancesFolder))
        {
            return Array.Empty<GroupedInstance>();
        }

        var instanceList = new Dictionary<string, GroupedInstance>();

        var instgroupsPath = Path.Combine(instancesFolder, "instgroups.json");

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
            
        foreach (var instanceFolder in Directory.EnumerateDirectories(instancesFolder))
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
        if (args.Game.PluginId != Id || Launcher == null)
        {
            yield break;
        }

        yield return new AutomaticPlayController(args.Game)
        {
            Type = AutomaticPlayActionType.File,
            TrackingMode = TrackingMode.Process,
            Path = Launcher.ExecutablePath,
            Arguments = @$"--launch ""{GetFolderName(args.Game)}""",
            WorkingDir = Launcher.InstallDirectory,
            Name = "Play with MultiMC"
        };
    }

    public override ISettings GetSettings(bool firstRunSettings) => SettingsViewModel;
    public override UserControl GetSettingsView(bool firstRunSettings) => new MultiMcLibrarySettingsView();
}