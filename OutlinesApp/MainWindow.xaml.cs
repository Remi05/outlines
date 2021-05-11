using System;
using System.Windows;
using System.Windows.Interop;
using OutlinesApp.ViewModels;
using Outlines;

namespace OutlinesApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IntPtr handle = new WindowInteropHelper(this).Handle;

            IOutlinesService outlinesService = new OutlinesService();
            IGlobalInputListener globalInputListener = new GlobalInputListener();
            InspectorViewModel inspectorViewModel = new InspectorViewModel(outlinesService, globalInputListener);
            PropertiesViewModel propertiesViewModel = new PropertiesViewModel(outlinesService);
            OutlinesViewModel outlinesViewModel = new OutlinesViewModel(this, outlinesService);

            var serviceContainer = ServiceContainer.Instance;
            serviceContainer.AddService(typeof(IOutlinesService), outlinesService);
            serviceContainer.AddService(typeof(IGlobalInputListener), globalInputListener);
            serviceContainer.AddService(typeof(InspectorViewModel), inspectorViewModel);
            serviceContainer.AddService(typeof(PropertiesViewModel), propertiesViewModel);
            serviceContainer.AddService(typeof(OutlinesViewModel), outlinesViewModel);
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
