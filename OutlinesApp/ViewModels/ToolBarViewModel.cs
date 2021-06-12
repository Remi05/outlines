using System.Diagnostics;

namespace OutlinesApp.ViewModels
{
    public class ToolBarViewModel
    {
        public InspectorViewModel InspectorViewModel { get; set; }   
        public RelayCommand<object> CloseAppCommand { get; private set; }
        public RelayCommand<object> GetHelpCommand { get; private set; }
        public RelayCommand<object> GiveFeedbackCommand { get; private set; }
        public RelayCommand<object> ShowMoreInfoCommand { get; private set; }

        public ToolBarViewModel(InspectorViewModel inspectorViewModel)
        {
            InspectorViewModel = inspectorViewModel;
            CloseAppCommand = new RelayCommand<object>(_ => App.Current.Shutdown());
            GetHelpCommand = new RelayCommand<object>(_ => GetHelp());
            GiveFeedbackCommand = new RelayCommand<object>(_ => GiveFeedback());
            ShowMoreInfoCommand = new RelayCommand<object>(_ => ShowMoreInfo());
        }

        private void GetHelp()
        {
            Process.Start("https://github.com/Remi05/outlines/wiki/User-Guide");
        }

        private void GiveFeedback()
        {
            Process.Start("https://github.com/Remi05/outlines/issues");
        }

        private void ShowMoreInfo()
        {
            Process.Start("https://github.com/Remi05/outlines");
        }
    }
}
