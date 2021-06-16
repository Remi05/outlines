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

            IColorPickerService colorPickerService = new ColorPickerService();
            IGlobalInputListener globalInputListener = new GlobalInputListener();
            IOutlinesService outlinesService = new OutlinesService();
            IScreenHelper screenHelper = new ScreenHelper(this);
            ColorPickerViewModel colorPickerViewModel = new ColorPickerViewModel(colorPickerService, globalInputListener);
            InspectorViewModel inspectorViewModel = new InspectorViewModel(outlinesService, globalInputListener);
            OverlayViewModel overlayViewModel = new OverlayViewModel(Dispatcher, screenHelper, outlinesService);
            PropertiesViewModel propertiesViewModel = new PropertiesViewModel(outlinesService);
            ToolBarViewModel toolBarViewModel = new ToolBarViewModel(inspectorViewModel);

            var serviceContainer = ServiceContainer.Instance;
            serviceContainer.AddService(typeof(IColorPickerService), colorPickerService);
            serviceContainer.AddService(typeof(IGlobalInputListener), globalInputListener);
            serviceContainer.AddService(typeof(IOutlinesService), outlinesService);
            serviceContainer.AddService(typeof(IScreenHelper), screenHelper);
            serviceContainer.AddService(typeof(ColorPickerViewModel), colorPickerViewModel);
            serviceContainer.AddService(typeof(InspectorViewModel), inspectorViewModel);
            serviceContainer.AddService(typeof(OverlayViewModel), overlayViewModel);
            serviceContainer.AddService(typeof(PropertiesViewModel), propertiesViewModel);
            serviceContainer.AddService(typeof(ToolBarViewModel), toolBarViewModel);
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
