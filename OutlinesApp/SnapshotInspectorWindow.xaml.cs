using System.Windows;
using Outlines;
using OutlinesApp.Services;
using OutlinesApp.ViewModels;

namespace OutlinesApp
{
    public partial class SnapshotInspectorWindow : Window
    {
        public SnapshotInspectorWindow(Snapshot snapshot)
        {
            InitializeComponent();

            IDistanceOutlinesProvider distanceOutlinesProvider = new DistanceOutlinesProvider();
            IElementProvider elementProvider = new CachedElementProvider(snapshot.UiTree);
            IOutlinesService outlinesService = new OutlinesService(distanceOutlinesProvider, elementProvider);
            IScreenHelper screenHelper = new CachedCoordinateConverter(snapshot.UiTree);
            OverlayViewModel overlayViewModel = new OverlayViewModel(Dispatcher, outlinesService, screenHelper);
            PropertiesViewModel propertiesViewModel = new PropertiesViewModel(outlinesService);
            SnapshotInspectorViewModel snapshotInspectorViewModel = new SnapshotInspectorViewModel(outlinesService, screenHelper);
            snapshotInspectorViewModel.Snapshot = snapshot;

            var serviceContainer = ServiceContainer.Instance;
            serviceContainer.AddService(typeof(OverlayViewModel), overlayViewModel);
            serviceContainer.AddService(typeof(PropertiesViewModel), propertiesViewModel);
            serviceContainer.AddService(typeof(SnapshotInspectorViewModel), snapshotInspectorViewModel);
        }
    }
}
