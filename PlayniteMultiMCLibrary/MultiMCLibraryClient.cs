using Playnite.SDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MultiMcLibrary
{
    public class MultiMcLibraryClient : LibraryClient
    {
        [DllImport("user32")]
        private static extern bool SetForegroundWindow(IntPtr hwnd);
        
        private readonly MultiMcLibrary _multiMcLibrary;

        private string MultiMcExe => Path.Combine(_multiMcLibrary.MultiMcPath, "MultiMC.exe");

        private bool? _cachedIsInstalled;
        public override bool IsInstalled => _cachedIsInstalled ?? !string.IsNullOrWhiteSpace(_multiMcLibrary.MultiMcPath) && (_cachedIsInstalled = File.Exists(MultiMcExe)).Value;

        public override string Icon { get; } = Path.Combine(MultiMcLibrary.AssemblyPath, "icon.png");

        public MultiMcLibraryClient(MultiMcLibrary multiMcLibrary)
        {
            _multiMcLibrary = multiMcLibrary;
        }

        public override void Open()
        {
            var mainProc = Process.GetProcessesByName("MultiMC Launcher").FirstOrDefault();
            if (mainProc is { HasExited: false })
            {
                SetForegroundWindow(mainProc.MainWindowHandle);
            }
            else
            {
                Process.Start(MultiMcExe);
            }
        }

        public override void Shutdown()
        {
            var mainProc = Process.GetProcessesByName("MultiMC Launcher").FirstOrDefault();
            if (mainProc is { HasExited: false })
            {
                mainProc.CloseMainWindow();
            }

            _multiMcLibrary.OnClientClosed();

            base.Shutdown();
        }
    }
}