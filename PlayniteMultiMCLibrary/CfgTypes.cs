using System;
using JetBrains.Annotations;

namespace MultiMcLibrary
{
    /// <summary>
    /// Sample:
    /// <code>
    /// ForgeVersion=
    /// InstanceType=OneSix
    /// IntendedVersion=
    /// JoinServerOnLaunch=false
    /// LWJGLVersion=
    /// LiteloaderVersion=
    /// LogPrePostOutput=true
    /// MCLaunchMethod=LauncherPart
    /// OverrideCommands=false
    /// OverrideConsole=false
    /// OverrideGameTime=false
    /// OverrideJavaArgs=false
    /// OverrideJavaLocation=false
    /// OverrideMCLaunchMethod=false
    /// OverrideMemory=false
    /// OverrideNativeWorkarounds=false
    /// OverrideWindow=false
    /// iconKey=diamond
    /// lastLaunchTime=1587493023072
    /// name=Beta 1.7.3
    /// notes=
    /// totalTimePlayed=19241
    /// </code>
    /// </summary>
    [UsedImplicitly]
    public class InstanceCfg
    {
        [CfgProperty("ForgeVersion")] public string? ForgeVersion { get; set; }
        [CfgProperty("InstanceType")] public string InstanceType { get; set; } = null!;
        [CfgProperty("IntendedVersion")] public string? IntendedVersion { get; set; }
        [CfgProperty("JoinServerOnLaunch")] public string? JoinServerOnLaunch { get; set; }
        [CfgProperty("LWJGLVersion")] public string? LwjglVersion { get; set; }
        [CfgProperty("LiteloaderVersion")] public string? LiteloaderVersion { get; set; }
        [CfgProperty("LogPrePostOutput")] public string? LogPrePostOutput { get; set; }
        [CfgProperty("MCLaunchMethod")] public string? McLaunchMethod { get; set; }
        [CfgProperty("OverrideCommands")] public bool? OverrideCommands { get; set; }
        [CfgProperty("OverrideConsole")] public bool? OverrideConsole { get; set; }
        [CfgProperty("OverrideGameTime")] public bool? OverrideGameTime { get; set; }
        [CfgProperty("OverrideJavaArgs")] public bool? OverrideJavaArgs { get; set; }
        [CfgProperty("OverrideJavaLocation")] public bool? OverrideJavaLocation { get; set; }
        [CfgProperty("OverrideMCLaunchMethod")] public bool? OverrideMcLaunchMethod { get; set; }
        [CfgProperty("OverrideMemory")] public bool? OverrideMemory { get; set; }
        [CfgProperty("OverrideNativeWorkarounds")] public bool? OverrideNativeWorkarounds { get; set; }
        [CfgProperty("OverrideWindow")] public bool? OverrideWindow { get; set; }
        [CfgProperty("iconKey")] public string IconKey { get; set; } = null!;
        
        /// <summary>
        /// In unix ms
        /// </summary>
        [CfgProperty("lastLaunchTime")] public ulong? LastLaunchTime { get; set; }
        
        /// <summary>
        /// Instance name
        /// </summary>
        [CfgProperty("name")] public string Name { get; set; } = null!;
        
        [CfgProperty("notes")] public string? Notes { get; set; }
        
        /// <summary>
        /// In seconds
        /// </summary>
        [CfgProperty("totalTimePlayed")] public ulong? TotalTimePlayed { get; set; }

        public DateTime? LastLaunchDateTime => LastLaunchTime != null
            ? DateTimeOffset.FromUnixTimeMilliseconds((long)LastLaunchTime).UtcDateTime
            : null;
    }

    public class CfgPropertyAttribute : Attribute
    {
        public string PropertyName { get; }

        public CfgPropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    [PublicAPI]
    public enum InstanceType
    {
        Legacy,
        /// <summary>
        /// Seems to be an alternative name for <see cref="OneSix"/>.
        /// </summary>
        Nostalgia,
        OneSix
    }
}