using System.ComponentModel;
using System.Windows;
using Outlines;
using OutlinesApp.Services;
using OutlinesApp.ViewModels;

namespace OutlinesApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            InputMaskingService inputMaskingService = new InputMaskingService(this);
            IColorPickerService colorPickerService = new ColorPickerService();
            IDistanceOutlinesProvider distanceOutlinesProvider = new DistanceOutlinesProvider();
            ITextPropertiesProvider textPropertiesProvider = new TextPropertiesProvider();
            IElementPropertiesProvider elementPropertiesProvider = new ElementPropertiesProvider(textPropertiesProvider);
            IElementProvider elementProvider = new LiveElementProvider(elementPropertiesProvider);
            IFolderConfig folderConfig = new FolderConfig();
            IGlobalInputListener globalInputListener = new GlobalInputListener(inputMaskingService);
            IOutlinesService outlinesService = new OutlinesService(distanceOutlinesProvider, elementProvider);
            ICoordinateConverter coordinateConverter = new LiveCoordinateConverter(this);
            IScreenHelper screenHelper = new ScreenHelper();
            IScreenshotService screenshotService = new ScreenshotService(App.Current.MainWindow.Hide, App.Current.MainWindow.Show);
            IUiTreeService uiTreeService = new UiTreeService(elementPropertiesProvider, outlinesService);
            ISnapshotService snapshotService = new SnapshotService(screenshotService, uiTreeService, folderConfig);
            ColorPickerViewModel colorPickerViewModel = new ColorPickerViewModel(colorPickerService, globalInputListener);
            InspectorViewModel inspectorViewModel = new InspectorViewModel(outlinesService, globalInputListener);
            OverlayViewModel overlayViewModel = new OverlayViewModel(Dispatcher, outlinesService, coordinateConverter, screenHelper);
            PropertiesViewModel propertiesViewModel = new PropertiesViewModel(outlinesService);
            ToolBarViewModel toolBarViewModel = new ToolBarViewModel(outlinesService, screenshotService, snapshotService, folderConfig, inspectorViewModel);
            UiTreeViewModel uiTreeViewModel = new UiTreeViewModel(Dispatcher, outlinesService, uiTreeService);

            var serviceContainer = ServiceContainer.Instance;
            serviceContainer.AddService(typeof(InputMaskingService), inputMaskingService);
            serviceContainer.AddService(typeof(IColorPickerService), colorPickerService);
            serviceContainer.AddService(typeof(IDistanceOutlinesProvider), distanceOutlinesProvider);
            serviceContainer.AddService(typeof(IElementProvider), elementProvider);
            serviceContainer.AddService(typeof(IElementPropertiesProvider), elementPropertiesProvider);
            serviceContainer.AddService(typeof(IFolderConfig), folderConfig);
            serviceContainer.AddService(typeof(ITextPropertiesProvider), textPropertiesProvider);
            serviceContainer.AddService(typeof(IGlobalInputListener), globalInputListener);
            serviceContainer.AddService(typeof(IOutlinesService), outlinesService);
            serviceContainer.AddService(typeof(IScreenshotService), screenshotService);
            serviceContainer.AddService(typeof(ISnapshotService), snapshotService);
            serviceContainer.AddService(typeof(IUiTreeService), uiTreeService);
            serviceContainer.AddService(typeof(ColorPickerViewModel), colorPickerViewModel);
            serviceContainer.AddService(typeof(InspectorViewModel), inspectorViewModel);
            serviceContainer.AddService(typeof(OverlayViewModel), overlayViewModel);
            serviceContainer.AddService(typeof(PropertiesViewModel), propertiesViewModel);
            serviceContainer.AddService(typeof(ToolBarViewModel), toolBarViewModel);
            serviceContainer.AddService(typeof(UiTreeViewModel), uiTreeViewModel);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var globalInputListener = ServiceContainer.Instance.GetService<IGlobalInputListener>();
            globalInputListener?.RegisterToInputEvents();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            var globalInputListener = ServiceContainer.Instance.GetService<IGlobalInputListener>();
            globalInputListener?.UnregisterFromInputEvents();
        }
    }
}
