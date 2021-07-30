using System.Windows;
using Outlines;
using OutlinesApp.Services;
using OutlinesApp.ViewModels;

namespace OutlinesApp
{
    public partial class SnapshotInspectorWindow : Window
    {
        public SnapshotInspectorWindow()
        {
            InitializeComponent();

            IDistanceOutlinesProvider distanceOutlinesProvider = new DistanceOutlinesProvider();
            IElementProvider elementProvider = new CachedElementProvider(null);
            IOutlinesService outlinesService = new OutlinesService(distanceOutlinesProvider, elementProvider);
            IScreenHelper screenHelper = new CachedCoordinateConverter();
            OverlayViewModel overlayViewModel = new OverlayViewModel(Dispatcher, outlinesService, screenHelper);
            PropertiesViewModel propertiesViewModel = new PropertiesViewModel(outlinesService);
            SnapshotInspectorViewModel snapshotInspectorViewModel = new SnapshotInspectorViewModel(outlinesService, screenHelper);

            var serviceContainer = ServiceContainer.Instance;

            serviceContainer.AddService(typeof(CachedElementProvider), elementProvider);
            serviceContainer.AddService(typeof(CachedCoordinateConverter), screenHelper);

            serviceContainer.AddService(typeof(OverlayViewModel), overlayViewModel);
            serviceContainer.AddService(typeof(PropertiesViewModel), propertiesViewModel);
            serviceContainer.AddService(typeof(SnapshotInspectorViewModel), snapshotInspectorViewModel);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var snapshot = Snapshot.LoadFromFile(@"C:\Users\remi_\OneDrive\Pictures\Outlines\snapshots\Snapshot-132719943527222607.json");
            
            var inspectorViewModel = ServiceContainer.Instance.GetService<SnapshotInspectorViewModel>();
            inspectorViewModel.Snapshot = snapshot;

            var elementProvider = ServiceContainer.Instance.GetService<CachedElementProvider>();
            elementProvider.UiTree = snapshot.UiTree;

            var coordinateConverter = ServiceContainer.Instance.GetService<CachedCoordinateConverter>();
            coordinateConverter.UiTree = snapshot.UiTree;
        }
    }
}
