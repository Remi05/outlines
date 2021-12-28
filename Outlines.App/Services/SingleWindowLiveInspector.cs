
namespace Outlines.App.Services
{
    public class SingleWindowLiveInspector : ILiveInspector
    {
        private LiveInspectorWindow LiveInspectorWindow { get; set; }

        public void Show()
        {
            LiveInspectorWindow = new LiveInspectorWindow();
            LiveInspectorWindow.Show();
        }

        public void Close()
        {
            LiveInspectorWindow?.Close();
        }
    }
}
