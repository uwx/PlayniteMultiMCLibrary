using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MultiMcLibrary;

public static class TokenFormatter
{
    private static readonly Dictionary<string, Func<InstanceCfg, MultiMcPack, string>> TokenConsumers =
        new()
        {
            ["InstanceName"] = (cfg, pack) => cfg.Name,
            ["MinecraftVersion"] = (cfg, pack) => pack.GetComponentById("net.minecraft")?.Version ?? string.Empty,
            ["LWJGLVersion"] = (cfg, pack) => (pack.GetComponentById("org.lwjgl") ?? pack.GetComponentById("org.lwjgl3"))?.Version ?? string.Empty,
            ["FabricVersion"] = (cfg, pack) => pack.GetComponentById("net.fabricmc.fabric-loader")?.Version ?? string.Empty,
        };

    public static IEnumerable<string> ValidTokens => TokenConsumers.Keys;

    private static readonly Regex TokenRegex = new($"{{({string.Join("|", ValidTokens.ToArray())})}}",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);
        
    public static string FormatString(string format, InstanceCfg instanceCfg, MultiMcPack pack)
    {
        return TokenRegex.Replace(format, match => TokenConsumers[match.Groups[1].Value](instanceCfg, pack));
    }
}