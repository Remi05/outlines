using System;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using Outlines.Core;
using Outlines.Inspection;
using Outlines.App.Services;
using Outlines.App.ViewModels;

namespace Outlines.App
{
    public partial class SnapshotInspectorWindow : Window
    {
        private TitlebarThemeManager TitlebarThemeManager { get; set; }

        public SnapshotInspectorWindow(Snapshot snapshot, ThemeManager themeManager)
        {
            InitializeComponent();

            IntPtr hwnd = new WindowInteropHelper(GetWindow(this)).EnsureHandle();
            TitlebarThemeManager = new TitlebarThemeManager(themeManager, new TitlebarHelper(), hwnd);

            IDistanceOutlinesProvider distanceOutlinesProvider = new DistanceOutlinesProvider();
            IElementProvider elementProvider = new CachedElementProvider(snapshot.UITree);
            IOutlinesService outlinesService = new OutlinesService(distanceOutlinesProvider, elementProvider);
            ICoordinateConverter coordinateConverter = new CachedCoordinateConverter(snapshot);
            IScreenHelper screenHelper = new ScreenHelper(this);
            IUITreeService uiTreeService = new CachedUITreeService(snapshot.UITree);

            OverlayViewModel overlayViewModel = new OverlayViewModel(Dispatcher, outlinesService, coordinateConverter, screenHelper);
            PropertiesViewModel propertiesViewModel = new PropertiesViewModel(outlinesService);
            UITreeViewModel uiTreeViewModel = new UITreeViewModel(Dispatcher, outlinesService, uiTreeService);
            SnapshotInspectorViewModel snapshotInspectorViewModel = new SnapshotInspectorViewModel(outlinesService, coordinateConverter);
            snapshotInspectorViewModel.Snapshot = snapshot;

            var serviceContainer = ServiceContainer.Instance;
            serviceContainer.AddService(typeof(OverlayViewModel), overlayViewModel);
            serviceContainer.AddService(typeof(PropertiesViewModel), propertiesViewModel);
            serviceContainer.AddService(typeof(SnapshotInspectorViewModel), snapshotInspectorViewModel);
            serviceContainer.AddService(typeof(UITreeViewModel), uiTreeViewModel);
        }
    }
}
