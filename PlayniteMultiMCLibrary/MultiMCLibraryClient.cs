using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MultiMcLibrary;

public class MultiMcLibraryClient : LibraryClient
{
    [DllImport("user32")]
    private static extern bool SetForegroundWindow(IntPtr hwnd);

    private readonly MultiMcLibrary _multiMcLibrary;

    private bool? _cachedIsInstalled;
    public override bool IsInstalled
        => _cachedIsInstalled ?? _multiMcLibrary.Launcher is { ExecutablePath: {} }
            && (_cachedIsInstalled = File.Exists(_multiMcLibrary.Launcher.ExecutablePath)).Value;

    public override string Icon => Path.Combine(MultiMcLibrary.AssemblyPath, _multiMcLibrary.Launcher?.IconName ?? "icon-multimc.png");

    public MultiMcLibraryClient(MultiMcLibrary multiMcLibrary)
    {
        _multiMcLibrary = multiMcLibrary;
    }

    public override void Open()
    {
        if (_multiMcLibrary.Launcher == null)
        {
            _multiMcLibrary.DisplayLauncherError();
            return;
        }
        
        var mainProc = Process.GetProcessesByName(_multiMcLibrary.Launcher.ProcessName).FirstOrDefault();
        if (mainProc is { HasExited: false })
        {
            SetForegroundWindow(mainProc.MainWindowHandle);
        }
        else
        {
            Process.Start(_multiMcLibrary.Launcher.ExecutablePath);
        }
    }

    public override void Shutdown()
    {
        if (_multiMcLibrary.Launcher == null)
        {
            _multiMcLibrary.DisplayLauncherError();
            return;
        }

        var mainProc = Process.GetProcessesByName(_multiMcLibrary.Launcher.ProcessName).FirstOrDefault();
        if (mainProc is { HasExited: false })
        {
            mainProc.CloseMainWindow();
        }

        _multiMcLibrary.OnClientClosed();

        base.Shutdown();
    }
}