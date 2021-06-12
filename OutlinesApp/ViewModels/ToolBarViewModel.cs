
namespace OutlinesApp.ViewModels
{
    public class ToolBarViewModel
    {
        public InspectorViewModel InspectorViewModel { get; set; }   
        public RelayCommand<object> CloseAppCommand { get; private set; }

        public ToolBarViewModel(InspectorViewModel inspectorViewModel)
        {
            InspectorViewModel = inspectorViewModel;
            CloseAppCommand = new RelayCommand<object>(_ => App.Current.Shutdown());
        }
    }
}
