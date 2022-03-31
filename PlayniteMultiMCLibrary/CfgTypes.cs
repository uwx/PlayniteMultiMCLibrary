using System;
using JetBrains.Annotations;

namespace MultiMcLibrary;

/// <summary>
/// Sample:
/// <code>
/// Analytics=false
/// AnalyticsClientID=aaa
/// AnalyticsSeen=2
/// ApplicationTheme=dark
/// AutoCloseConsole=false
/// AutoUpdate=true
/// CentralModsDir=mods
/// ConsoleFont=Courier
/// ConsoleFontSize=10
/// ConsoleMaxLines=100000
/// ConsoleOverflowStop=true
/// ConsoleWindowGeometry=aaa
/// ConsoleWindowState=aaa
/// IconTheme=pe_colored
/// IconsDir=icons
/// InstSortMode=Name
/// InstanceDir=instances
/// JProfilerPath=
/// JVisualVMPath=
/// JavaArchitecture=64
/// JavaPath=C:/SSDPrograms/GraalVM CE J8 20.2.0/bin/javaw.exe
/// JavaTimestamp=1597578006000
/// JavaVersion=1.8.0_262
/// JsonEditor=
/// JvmArgs=
/// Language=en_US
/// LastHostname=aaa
/// LastUsedGroupForNewInstance=Forge
/// LaunchMaximized=false
/// MCEditPath=
/// MainWindowGeometry=aaa
/// MainWindowState=aaa
/// MaxMemAlloc=8192
/// MinMemAlloc=1024
/// MinecraftWinHeight=480
/// MinecraftWinWidth=854
/// NewInstanceGeometry=aaa
/// PagedGeometry=aaa
/// PasteEEAPIKey=multimc
/// PermGen=128
/// PostExitCommand=
/// PreLaunchCommand=
/// ProxyAddr=127.0.0.1
/// ProxyPass=
/// ProxyPort=8080
/// ProxyType=None
/// ProxyUser=
/// RecordGameTime=true
/// SelectedInstance=
/// ShowConsole=false
/// ShowConsoleOnError=true
/// ShowGameTime=true
/// ShowGlobalGameTime=true
/// ShownNotifications=
/// UpdateChannel=develop
/// UpdateDialogGeometry=aaa
/// UseNativeGLFW=false
/// UseNativeOpenAL=false
/// WrapperCommand=
/// </code>
/// </summary>
[UsedImplicitly]
public class MultiMcCfg
{
    [CfgProperty("InstanceDir")] public string InstanceDir { get; set; } = "instances";
}

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