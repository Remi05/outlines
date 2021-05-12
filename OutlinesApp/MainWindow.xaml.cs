using System;
using System.Windows;
using System.Windows.Interop;
using Outlines;
using OutlinesApp.ViewModels;
using OutlinesApp.Services;

namespace OutlinesApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            IOutlinesService outlinesService = new OutlinesService();
            IGlobalInputListener globalInputListener = new GlobalInputListener();
            IScreenHelper screenHelper = new ScreenHelper(this);
            InspectorViewModel inspectorViewModel = new InspectorViewModel(outlinesService, globalInputListener);
            PropertiesViewModel propertiesViewModel = new PropertiesViewModel(outlinesService);
            OverlayViewModel overlayViewModel = new OverlayViewModel(Dispatcher, screenHelper, outlinesService);

            var serviceContainer = ServiceContainer.Instance;
            serviceContainer.AddService(typeof(IOutlinesService), outlinesService);
            serviceContainer.AddService(typeof(IGlobalInputListener), globalInputListener);
            serviceContainer.AddService(typeof(IScreenHelper), screenHelper);
            serviceContainer.AddService(typeof(InspectorViewModel), inspectorViewModel);
            serviceContainer.AddService(typeof(PropertiesViewModel), propertiesViewModel);
            serviceContainer.AddService(typeof(OverlayViewModel), overlayViewModel);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var globalInputListener = ServiceContainer.Instance.GetService<IGlobalInputListener>();
            globalInputListener?.RegisterToInputEvents();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            var globalInputListener = ServiceContainer.Instance.GetService<IGlobalInputListener>();
            globalInputListener?.UnregisterFromInputEvents();
        }
    }
}
