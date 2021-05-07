using Outlines;

namespace OutlinesApp.ViewModels
{
    public class PropertiesViewModel
    {
        protected IOutlinesService OutlinesService { get; set; }
        public ElementProperties ElementProperties { get; protected set; }
        public TextProperties TextProperties { get; protected set; }
    
        public PropertiesViewModel(IOutlinesService outlinesService)
        {
            OutlinesService = outlinesService;
            OutlinesService.SelectedElementChanged += OnSelectedElementChanged;
        }

        protected void OnSelectedElementChanged()
        {
            ElementProperties = OutlinesService.SelectedElementProperties;
            TextProperties = OutlinesService.SelectedTextProperties;
        }
    }
}
