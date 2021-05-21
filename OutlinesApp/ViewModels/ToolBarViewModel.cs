
namespace OutlinesApp.ViewModels
{
    public class ToolBarViewModel
    {
        private InspectorViewModel InspectorViewModel { get; set; }
        
        public RelayCommand<object> CloseAppCommand { get; private set; }
        public RelayCommand<object> OpenSettingsCommand { get; private set; }
        public RelayCommand<object> ToggleOverlayCommand { get; private set; }
        public RelayCommand<object> TogglePropertiesPanelCommand { get; private set; }

        public ToolBarViewModel(InspectorViewModel inspectorViewModel)
        {
            InspectorViewModel = inspectorViewModel;
            CloseAppCommand = new RelayCommand<object>(_ => App.Current.Shutdown());
            OpenSettingsCommand = new RelayCommand<object>(_ => { });
            ToggleOverlayCommand = new RelayCommand<object>(_ => InspectorViewModel.ToggleOverlay());
            TogglePropertiesPanelCommand = new RelayCommand<object>(_ => InspectorViewModel.TogglePropertiesPanel());
        }
    }
}
