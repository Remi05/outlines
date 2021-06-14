using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OutlinesApp.ViewModels
{
    public class UiTreeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<UiTreeItemViewModel> Elements { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public UiTreeViewModel()
        {
        }

        public void OnElementSelectionChanged(UiTreeItemViewModel uiTreeItemViewModel)
        {

        }
    }
}
