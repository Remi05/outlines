using System.IO;
using System.Windows;
using Outlines.Core;
using Outlines.App.Services;
using Microsoft.Toolkit.Uwp.Notifications;

namespace Outlines.App
{
    public partial class App : Application
    {
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

            string snapshotFileToOpen = e.Args.Length > 0 ? e.Args[0] : null;

            if (!string.IsNullOrWhiteSpace(snapshotFileToOpen) && File.Exists(snapshotFileToOpen))
            {
                var snapshot = Snapshot.LoadFromFile(snapshotFileToOpen);
                var window = new SnapshotInspectorWindow(snapshot, ThemeManager);
                window.Show();
            }
            else
            {
                LiveInspector = new MultiWindowLiveInspector();
                LiveInspector.Show();
            }
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
