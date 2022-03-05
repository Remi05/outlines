using System.IO;
using System.Windows;
using Outlines.Core;
using Outlines.App.Services;

namespace Outlines.App
{
    public partial class App : Application
    {
        private ThemeManager ThemeManager { get; set; }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            string fileToOpen = e.Args.Length > 0 ? e.Args[0] : null;

            ThemeManager = new ThemeManager();

            if (!string.IsNullOrWhiteSpace(fileToOpen) && File.Exists(fileToOpen))
            {
                var snapshot = Snapshot.LoadFromFile(fileToOpen);
                Window window = new SnapshotInspectorWindow(snapshot, ThemeManager);
                window.Show();
            }
            else
            {
                Window window = new LiveInspectorWindow();
                window.Show();
            }

        }
    }
}
