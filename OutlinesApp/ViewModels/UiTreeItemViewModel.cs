using Outlines;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OutlinesApp.ViewModels
{
    public class UiTreeItemViewModel : INotifyPropertyChanged
    {
        public ElementProperties ElementProperties { get; private set; }

        public ObservableCollection<UiTreeItemViewModel> ChildrenElements { get; private set; } 

        public event PropertyChangedEventHandler PropertyChanged;

        public UiTreeItemViewModel(ElementProperties elementProperties)
        {
            ElementProperties = elementProperties;
        }
    }
}
