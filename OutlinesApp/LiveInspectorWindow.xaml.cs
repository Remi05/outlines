using System;
using System.ComponentModel;
using System.Windows;
using Outlines;
using OutlinesApp.Services;
using OutlinesApp.ViewModels;

namespace OutlinesApp
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
            ITextPropertiesProvider textPropertiesProvider = new TextPropertiesProvider();
            IElementPropertiesProvider elementPropertiesProvider = new ElementPropertiesProvider(textPropertiesProvider);
            IElementProvider elementProvider = new LiveElementProvider(elementPropertiesProvider);
            IFolderConfig folderConfig = new FolderConfig();
            IOutlinesService outlinesService = new OutlinesService(distanceOutlinesProvider, elementProvider);
            ICoordinateConverter coordinateConverter = new LiveCoordinateConverter(this);
            IScreenHelper screenHelper = new ScreenHelper(this);
            IScreenshotService screenshotService = new ScreenshotService(App.Current.MainWindow.Hide, App.Current.MainWindow.Show);
            IUiTreeService uiTreeService = new LiveUiTreeService(elementPropertiesProvider, outlinesService);
            ISnapshotService snapshotService = new SnapshotService(screenshotService, uiTreeService, screenHelper, folderConfig);

            ColorPickerViewModel colorPickerViewModel = new ColorPickerViewModel(colorPickerService, GlobalInputListener);
            InspectorViewModel inspectorViewModel = new InspectorViewModel(outlinesService, GlobalInputListener);
            OverlayViewModel overlayViewModel = new OverlayViewModel(Dispatcher, outlinesService, coordinateConverter, screenHelper);
            PropertiesViewModel propertiesViewModel = new PropertiesViewModel(outlinesService);
            ToolBarViewModel toolBarViewModel = new ToolBarViewModel(outlinesService, screenshotService, snapshotService, folderConfig, inspectorViewModel);
            UiTreeViewModel uiTreeViewModel = new UiTreeViewModel(Dispatcher, outlinesService, uiTreeService);

            var serviceContainer = ServiceContainer.Instance;
            serviceContainer.AddService(typeof(InputMaskingService), inputMaskingService);
            serviceContainer.AddService(typeof(ColorPickerViewModel), colorPickerViewModel);
            serviceContainer.AddService(typeof(InspectorViewModel), inspectorViewModel);
            serviceContainer.AddService(typeof(OverlayViewModel), overlayViewModel);
            serviceContainer.AddService(typeof(PropertiesViewModel), propertiesViewModel);
            serviceContainer.AddService(typeof(ToolBarViewModel), toolBarViewModel);
            serviceContainer.AddService(typeof(UiTreeViewModel), uiTreeViewModel);
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
            focusHelper.DisableTakingFocus(this);
        }
    }
}
