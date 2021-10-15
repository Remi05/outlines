using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interop;
using Outlines.Core;
using Outlines.Inspection;
using Outlines.Inspection.NetFramework;
using Outlines.App.Services;
using Outlines.App.ViewModels;

namespace Outlines.App
{
    public partial class LiveInspectorWindow : Window
    {
        private IGlobalInputListener GlobalInputListener { get; set; }

        public LiveInspectorWindow()
        {
            InitializeComponent();

            InputMaskingService inputMaskingService = new InputMaskingService(this);
            GlobalInputListener = new GlobalInputListener(inputMaskingService);

            IColorPickerService colorPickerService = new ColorPickerService();
            IDistanceOutlinesProvider distanceOutlinesProvider = new DistanceOutlinesProvider();
            IElementPropertiesProvider elementPropertiesProvider = new ElementPropertiesProvider();
            IElementProvider elementProvider = new FilteredLiveElementProvider(elementPropertiesProvider);
            IFolderConfig folderConfig = new FolderConfig();
            IOutlinesService outlinesService = new OutlinesService(distanceOutlinesProvider, elementProvider);
            ICoordinateConverter coordinateConverter = new LiveCoordinateConverter(this);
            IScreenHelper screenHelper = new ScreenHelper(this);
            IScreenshotService screenshotService = new ScreenshotService(App.Current.MainWindow.Hide, App.Current.MainWindow.Show);
            IUITreeService uiTreeService = new LiveUITreeService(elementPropertiesProvider, outlinesService);
            ISnapshotService snapshotService = new SnapshotService(screenshotService, uiTreeService, screenHelper, folderConfig);

            ColorPickerViewModel colorPickerViewModel = new ColorPickerViewModel(colorPickerService, GlobalInputListener);
            InspectorViewModel inspectorViewModel = new InspectorViewModel(outlinesService, GlobalInputListener);
            OverlayViewModel overlayViewModel = new OverlayViewModel(Dispatcher, outlinesService, coordinateConverter, screenHelper);
            PropertiesViewModel propertiesViewModel = new PropertiesViewModel(outlinesService);
            ToolBarViewModel toolBarViewModel = new ToolBarViewModel(outlinesService, screenshotService, snapshotService, folderConfig, coordinateConverter, inspectorViewModel);
            UITreeViewModel uiTreeViewModel = new UITreeViewModel(Dispatcher, outlinesService, uiTreeService);

            var serviceContainer = ServiceContainer.Instance;
            serviceContainer.AddService(typeof(InputMaskingService), inputMaskingService);
            serviceContainer.AddService(typeof(ColorPickerViewModel), colorPickerViewModel);
            serviceContainer.AddService(typeof(InspectorViewModel), inspectorViewModel);
            serviceContainer.AddService(typeof(OverlayViewModel), overlayViewModel);
            serviceContainer.AddService(typeof(PropertiesViewModel), propertiesViewModel);
            serviceContainer.AddService(typeof(ToolBarViewModel), toolBarViewModel);
            serviceContainer.AddService(typeof(UITreeViewModel), uiTreeViewModel);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            GlobalInputListener?.RegisterToInputEvents();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            GlobalInputListener?.UnregisterFromInputEvents();
        }

        protected void OnSourceInitialized(object sender, EventArgs e)
        {
            var focusHelper = new FocusHelper();
            focusHelper.DisableTakingFocus(new WindowInteropHelper(this).Handle);
        }
    }
}
