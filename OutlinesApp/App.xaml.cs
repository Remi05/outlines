using System.IO;
using System.Windows;
using Outlines;

namespace OutlinesApp
{
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            string fileToOpen = e.Args.Length > 0 ? e.Args[0] : null;

            Window window;
            if (!string.IsNullOrWhiteSpace(fileToOpen) && File.Exists(fileToOpen))
            {
                var snapshot = Snapshot.LoadFromFile(fileToOpen);
                window = new SnapshotInspectorWindow(snapshot);
            }
            else
            {
                window = new LiveInspectorWindow();
            }
            window.Show();
        }
    }
}
