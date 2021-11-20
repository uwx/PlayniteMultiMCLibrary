using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MultiMcLibrary
{
    public static class DescriptionFormatter
    {
        private readonly record struct Mod(string Modid, string? Name, string? Version);

        private static readonly ConcurrentDictionary<(string FileName, long Size, DateTime LastModified), IReadOnlyList<Mod>> ModInfoCache = new();

        // https://stackoverflow.com/a/249937 Regex for quoted string with escaping quotes
        private static readonly Regex ModidRegex = new(@"""modid"":\s*""((?:[^""\\]|\.)*)""", RegexOptions.Compiled);
        private static readonly Regex NameRegex = new(@"""name"":\s*""((?:[^""\\]|\\.)*)""", RegexOptions.Compiled);
        private static readonly Regex VersionRegex = new(@"""version"":\s*""((?:[^""\\]|\\.)*)""", RegexOptions.Compiled);

        public static string FormatDescription(string instanceFolderPath, InstanceCfg cfg, MultiMcPack pack)
        {
            var sb = new StringBuilder();

            if (pack.GetComponentById("net.minecraft") is {} minecraft)
            {
                sb.Append($"<b>{minecraft.CachedName ?? "Minecraft"} {minecraft.Version}</b><br><br>");
            }

            if (!string.IsNullOrWhiteSpace(cfg.Notes))
            {
                sb.Append(cfg.Notes!.Replace("\n", "<br>")).Append("<br><br>");
            }
            
            if (pack.GetComponentById("org.lwjgl3") is {} lwjgl)
            {
                sb.Append($"LWJGL {lwjgl.Version}<br>");
            }
            
            if (pack.GetComponentById("org.lwjgl") is {} lwjgl2)
            {
                sb.Append($"LWJGL {lwjgl2.Version}<br>");
            }
            
            if (pack.GetComponentById("net.fabricmc.fabric-loader") is {} fabric)
            {
                sb.Append($"{fabric.CachedName ?? "Fabric Loader"} {fabric.Version}<br>");
            }
            
            if (pack.GetComponentById("net.fabricmc.intermediary") is {} fabricIntermediary)
            {
                sb.Append($"Fabric {fabricIntermediary.CachedName ?? "Intermediary Mappings"} {fabricIntermediary.Version}<br>");
            }
            
            if (pack.GetComponentById("net.minecraftforge") is {} forge)
            {
                sb.Append($"{forge.CachedName ?? "Forge"} {forge.Version}<br>");
            }

            sb.Append("<br>");

            var jarmods = pack.Components
                .Where(e => e.Uid.StartsWith("org.multimc.jarmod"))
                .ToArray();

            if (jarmods.Length > 0)
            {
                var enabledCount = jarmods.Count(e => !(e.Disabled ?? false));
                sb.Append($"{enabledCount} jar mods enabled out of {jarmods.Length}<br>");

                foreach (var component in jarmods)
                {
                    sb.Append(component.Disabled ?? false ? "❎ " : "✅ ");

                    sb.Append(component.CachedName);
                    if (component.Version != null)
                    {
                        sb.Append(' ').Append(component.Version);
                    }

                    sb.Append("<br>");
                }
            }

            // Loader mods
            var loaderModsFolder = Path.Combine(instanceFolderPath, ".minecraft/mods");
            if (Directory.Exists(loaderModsFolder))
            {
                var tasks = Directory.EnumerateFiles(loaderModsFolder)
                    .Where(file => file.EndsWith(".jar") || file.EndsWith(".jar.disabled"))
                    .Select(FetchModsAsync)
                    .ToArray();

                // ReSharper disable once CoVariantArrayConversion
                Task.WaitAll(tasks);

                foreach (var (enabled, mods) in tasks.Select(e => e.Result))
                {
                    foreach (var (modid, name, version) in mods)
                    {
                        sb.Append(enabled ? "✅ " : "❎ ");
                        sb.Append(name ?? modid).Append(' ').Append(version ?? "");
                        sb.Append("<br>");
                    }
                }
            }

            // TODO coremods

            return sb.ToString();
        }

        private static Task<(bool Enabled, IReadOnlyList<Mod> Mods)> FetchModsAsync(string file)
        {
            var fileInfo = new FileInfo(file);
            var cleanName = Path.GetFileName(file)
                .Replace(".jar.disabled", "")
                .Replace(".jar", "");
            var cacheKey = (fileName: cleanName, fileInfo.Length, fileInfo.LastWriteTime);

            var enabled = file.EndsWith(".jar");

            if (ModInfoCache.TryGetValue(cacheKey, out var cachedModInfo))
            {
                return Task.FromResult((Enabled: enabled, Mods: cachedModInfo));
            }

            return Task.Run(async () =>
            {
                try
                {
                    var mods = await LoadModsFromJarAsync(file, cleanName);

                    ModInfoCache.TryAdd(cacheKey, mods);

                    return (Enabled: enabled, Mods: mods);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"During processing of loader mod {file}", ex);
                }
            });
        }

        private static async Task<IReadOnlyList<Mod>> LoadModsFromJarAsync(string file, string cleanName)
        {
            using var archive = ZipFile.OpenRead(file);

            if (archive.Entries.FirstOrDefault(e => e.FullName == "fabric.mod.json") is { } fabricModEntry)
            {
                // load fabric modinfo

                using var entryStream = fabricModEntry.Open();
                using var reader = new StreamReader(entryStream);

                var json = await reader.ReadToEndAsync() ?? "{}";

                var fabricMod = JsonConvert.DeserializeObject<FabricMod>(json);

                return new Mod[] { new(fabricMod.Id, fabricMod.Name, fabricMod.Version) };
            }

            if (archive.Entries.FirstOrDefault(e => e.FullName == "mcmod.info") is { } mcmodEntry)
            {
                // load forge modinfo

                using var entryStream = mcmodEntry.Open();
                using var reader = new StreamReader(entryStream);

                var json = await reader.ReadToEndAsync();

                if (json == null)
                {
                    // no modinfo exists, just add filename
                    return new Mod[] { new(cleanName, null, null) };
                }

                json = json.TrimStart();

                try
                {
                    if (json.StartsWith("{"))
                    {
                        // load old modinfo format
                        var oldModInfo = JsonConvert.DeserializeObject<OldMcmodInfo>(json);

                        return oldModInfo.ModList.Select(e => new Mod(e.Modid, e.Name, e.Version)).ToArray();
                    }
                    else
                    {
                        // load new modinfo format
                        var newModInfos = JsonConvert.DeserializeObject<McModInfo[]>(json);

                        return newModInfos.Select(e => new Mod(e.Modid, e.Name, e.Version)).ToArray();
                    }
                }
                catch (JsonReaderException) // Bad JSON format for modinfo, this is allowed but I don't know how to handle it
                {
                    var modid = ModidRegex.Match(json).Groups?[1].Value;
                    var name = NameRegex.Match(json).Groups?[1].Value;
                    var version = VersionRegex.Match(json).Groups?[1].Value;

                    if (!string.IsNullOrWhiteSpace(modid))
                    {
                        return new Mod[] { new(modid!, name, version) };
                    }

                    // no bruteforce regex match, just add filename
                    return new Mod[] { new(cleanName, null, null) };
                }
            }
            
            // no applicable modinfo exists, just add filename
            return new Mod[] { new(cleanName, null, null) };
        }
    }
}