using System.IO;
using System.Windows;
using Outlines.Core;
using Outlines.App.Services;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Outlines.App
{
    public partial class App : Application
    {
        private const string OutlinesProcotol = "outlines:";

        private ThemeManager ThemeManager { get; set; }
        private ILiveInspector LiveInspector { get; set; }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            ThemeManager = new ThemeManager();

            ToastNotificationManagerCompat.OnActivated += OnToastActivated;
            if (ToastNotificationManagerCompat.WasCurrentProcessToastActivated())
            {
                // We were activated by a toast notification, we'll wait for OnToastActivated to be
                // called to show the Snapshot inspector, so we don't want to show the live inspector.
                return;
            }

            string launchArg = e.Args.Length > 0 ? e.Args[0] : null;

            if (!string.IsNullOrWhiteSpace(launchArg))
            {
                string snapshotFileToOpen = GetSnapshotFilePathFromLaunchArg(launchArg);
                if (File.Exists(snapshotFileToOpen))
                {
                    var snapshot = Snapshot.LoadFromFile(snapshotFileToOpen);
                    var window = new SnapshotInspectorWindow(snapshot, ThemeManager);
                    window.Show();
                }
                else
                {
                    // The provided Snapshot file path is invalid, we should exit (an exit code of 1 indicates an error).
                    App.Current.Shutdown(1);
                }
            }
            else
            {
                LiveInspector = new MultiWindowLiveInspector();
                LiveInspector.Show();
            }
        }

        private string GetSnapshotFilePathFromLaunchArg(string launchArg)
        {
            if (launchArg.StartsWith(OutlinesProcotol))
            {
                // In protocol activation scenarios, we expect the URI to have the format "outlines:<snapshot file path>".
                return launchArg.Replace(OutlinesProcotol, "");
            }
            // In file activation scenarios, the launch argument should be the file path itself.
            return launchArg;
        }

        private void OnToastActivated(ToastNotificationActivatedEventArgsCompat toastArgs)
        {
            string snapshotFileToOpen = toastArgs.Argument;

            if (!string.IsNullOrWhiteSpace(snapshotFileToOpen) && File.Exists(snapshotFileToOpen))
            {
                var snapshot = Snapshot.LoadFromFile(snapshotFileToOpen);

                Dispatcher.Invoke(() =>
                {
                    var window = new SnapshotInspectorWindow(snapshot, ThemeManager);
                    window.Show();
                });
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            LiveInspector?.Close();
            base.OnExit(e);
        }
    }
}
