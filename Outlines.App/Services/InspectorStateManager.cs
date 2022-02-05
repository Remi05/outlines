
namespace Outlines.App.Services
{
    public class InspectorStateManager : IInspectorStateManager
    {
        private bool isOverlayVisible = true;
        public bool IsOverlayVisible
        {
            get => isOverlayVisible;
            set
            {
                if (value != isOverlayVisible)
                {
                    isOverlayVisible = value;
                    IsOverlayVisibleChanged?.Invoke(IsOverlayVisible);
                }
            }
        }

        private bool isPropertiesPanelVisible = true;
        public bool IsPropertiesPanelVisible
        {
            get => isPropertiesPanelVisible;
            set
            {
                if (value != isPropertiesPanelVisible)
                {
                    isPropertiesPanelVisible = value;
                    IsPropertiesPanelVisibleChanged?.Invoke(IsPropertiesPanelVisible);
                }
            }
        }

        private bool isTreeViewVisible = true;
        public bool IsTreeViewVisible
        {
            get => isTreeViewVisible;
            set
            {
                if (value != isTreeViewVisible)
                {
                    isTreeViewVisible = value;
                    IsTreeViewVisibleChanged?.Invoke(IsTreeViewVisible);
                }
            }
        }

        private bool isBackdropVisible = false;
        public bool IsBackdropVisible
        {
            get => isBackdropVisible;
            set
            {
                if (value != isBackdropVisible)
                {
                    isBackdropVisible = value;
                    IsBackdropVisibleChanged?.Invoke(IsBackdropVisible);
                }
            }
        }

        public event IsOverlayVisibleChangedHandler IsOverlayVisibleChanged;
        public event IsPropertiesPanelVisibleChangedHandler IsPropertiesPanelVisibleChanged;
        public event IsTreeViewVisibleChangedHandler IsTreeViewVisibleChanged;
        public event IsBackdropVisibleChangedHandler IsBackdropVisibleChanged;
    }
}
