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
            ICoordinateConverter coordinateConverter = new CachedCoordinateConverter(snapshot.UiTree);
            IScreenHelper screenHelper = new ScreenHelper();
            OverlayViewModel overlayViewModel = new OverlayViewModel(Dispatcher, outlinesService, coordinateConverter, screenHelper);
            PropertiesViewModel propertiesViewModel = new PropertiesViewModel(outlinesService);
            SnapshotInspectorViewModel snapshotInspectorViewModel = new SnapshotInspectorViewModel(outlinesService, coordinateConverter);
            snapshotInspectorViewModel.Snapshot = snapshot;

            var serviceContainer = ServiceContainer.Instance;
            serviceContainer.AddService(typeof(OverlayViewModel), overlayViewModel);
            serviceContainer.AddService(typeof(PropertiesViewModel), propertiesViewModel);
            serviceContainer.AddService(typeof(SnapshotInspectorViewModel), snapshotInspectorViewModel);
        }
    }
}
