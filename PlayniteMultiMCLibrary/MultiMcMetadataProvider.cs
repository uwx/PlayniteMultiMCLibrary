using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Playnite.SDK;
using Playnite.SDK.Models;

namespace MultiMcLibrary;

public class MultiMcMetadataProvider : LibraryMetadataProvider
{
    private static readonly ILogger Logger = LogManager.GetLogger();
    private readonly MultiMcLibrary _library;

    private static readonly Regex PluginProvidedImageRegex = new(@"^(\d+\.\d+)\.png$", RegexOptions.Compiled | RegexOptions.ECMAScript);

    private readonly record struct Version(string VersionString)
    {
        public bool IsMatch(string targetVersion)
            => VersionString == targetVersion || targetVersion.StartsWith(VersionString + '.');
    }

    private static readonly IReadOnlyDictionary<Version, string> PluginProvidedCovers = (
        from file in Directory.EnumerateFiles(Path.Combine(MultiMcLibrary.AssemblyPath, "covers"))
        let match = PluginProvidedImageRegex.Match(Path.GetFileName(file))
        where match.Success
        select (version: new Version(match.Groups[1].Value), file)
    ).ToDictionary(e => e.version, e => e.file);

    private static readonly IReadOnlyDictionary<Version, string> PluginProvidedBackgrounds = (
        from file in Directory.EnumerateFiles(Path.Combine(MultiMcLibrary.AssemblyPath, "backgrounds"))
        let match = PluginProvidedImageRegex.Match(Path.GetFileName(file))
        where match.Success
        select (version: new Version(match.Groups[1].Value), file)
    ).ToDictionary(e => e.version, e => e.file);

    private static readonly string DefaultCover = Path.Combine(MultiMcLibrary.AssemblyPath, "covers/default.png");
    private static readonly string DefaultBackground = Path.Combine(MultiMcLibrary.AssemblyPath, "backgrounds/default.png");

    public MultiMcMetadataProvider(MultiMcLibrary library)
    {
        _library = library;
    }

    public override GameMetadata GetMetadata(Game game)
    {
        if (!_library.TryGetInstanceInfo(game, out var cfg, out var pack, out var instanceFolder))
        {
            return new GameMetadata();
        }

        // ReSharper disable once UseObjectOrCollectionInitializer
        var metadata = new GameMetadata();
        metadata.Description = DescriptionFormatter.FormatDescription(instanceFolder, cfg, pack);
        metadata.Icon = GetValidIcon(cfg);

        var gameVersion = pack.GetComponentById("net.minecraft")?.Version;

        if (_library.Settings.UseVersionCovers && GetPluginCover(gameVersion) is { } existingCover)
        {
            metadata.CoverImage = existingCover;
        }
        else if (_library.Settings.UseDefaultCover)
        {
            metadata.CoverImage = new MetadataFile(DefaultCover);
        }

        if (_library.Settings.UseVersionBackgrounds && GetPluginBackground(gameVersion) is { } existingBackground)
        {
            metadata.BackgroundImage = existingBackground;
        }
        else if (_library.Settings.UseDefaultBackground)
        {
            metadata.BackgroundImage = new MetadataFile(DefaultBackground);
        }

        return metadata;
    }

    private MetadataFile? GetValidIcon(InstanceCfg instanceCfg)
    {
        var pathOptions = new[]
        {
            _library.Launcher != null ? Path.Combine(_library.Launcher.InstallDirectory, $"icons/{instanceCfg.IconKey}.png") : null,
            Path.Combine(MultiMcLibrary.AssemblyPath, $"icons/{instanceCfg.IconKey}.png"),
        };

        // Logger.Info(Path.Combine(_library.MultiMcPath, $"icons/{instanceCfg.IconKey}.png"));
        // Logger.Info(Path.Combine(MultiMcLibrary.AssemblyPath, $"icons/{instanceCfg.IconKey}.png"));

        var foundPath = pathOptions.FirstOrDefault(File.Exists);
        return foundPath != null ? new MetadataFile(foundPath) : null;
    }

    private static MetadataFile? GetPluginCover(string? version)
    {
        if (version == null)
        {
            return null;
        }
            
        foreach (var (aversion, path) in PluginProvidedCovers)
        {
            if (aversion.IsMatch(version))
            {
                return new MetadataFile(path);
            }
        }

        return null;
    }

    private static MetadataFile? GetPluginBackground(string? version)
    {
        if (version == null)
        {
            return null;
        }
            
        foreach (var (aversion, path) in PluginProvidedBackgrounds)
        {
            if (aversion.IsMatch(version))
            {
                return new MetadataFile(path);
            }
        }

        return null;
    }
}