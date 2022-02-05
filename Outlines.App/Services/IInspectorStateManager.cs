using System;

namespace Outlines.App.Services
{
    public delegate void IsOverlayVisibleChangedHandler(bool isOverlayVisible);
    public delegate void IsPropertiesPanelVisibleChangedHandler(bool isPropertiesPanelVisible);
    public delegate void IsTreeViewVisibleChangedHandler(bool isTreeViewVisible);
    public delegate void IsBackdropVisibleChangedHandler(bool isBackdropVisible);

    public interface IInspectorStateManager
    {
        bool IsOverlayVisible { get; set; }
        bool IsPropertiesPanelVisible { get; set; }
        bool IsTreeViewVisible { get; set; }
        bool IsBackdropVisible { get; set; }

        event IsOverlayVisibleChangedHandler IsOverlayVisibleChanged;
        event IsPropertiesPanelVisibleChangedHandler IsPropertiesPanelVisibleChanged;
        event IsTreeViewVisibleChangedHandler IsTreeViewVisibleChanged;
        event IsBackdropVisibleChangedHandler IsBackdropVisibleChanged;
    }
}
