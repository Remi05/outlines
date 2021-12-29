using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using Outlines.App.ViewModels;
using Outlines.Core;
using Outlines.Inspection;
using Outlines.Inspection.NetFramework;

namespace Outlines.App.Services
{
    public class MultiWindowLiveInspector : ILiveInspector
    {
        private const int TargetHoverDelayInMs = 75;

        private ICoordinateConverter CoordinateConverter { get; set; }
        private IGlobalInputListener GlobalInputListener { get; set; }
        private IInspectorStateManager InspectorStateManager { get; set; }
        private IOutlinesService OutlinesService { get; set; }
        private IScreenHelper ScreenHelper { get; set; } 
        private UiaWindowHelper UiaWindowHelper { get; set; } = new UiaWindowHelper();

        private ToolBarWindow ToolBarWindow { get; set; }
        private OverlayWindow OverlayWindow { get; set; }
        private PropertiesWindow PropertiesWindow { get; set; }
        private TreeViewWindow TreeViewWindow { get; set; }

        private ISet<Window> RenderedWindows { get; set; } = new HashSet<Window>();

        public MultiWindowLiveInspector()
        {
            CreateWindows();
            InitializeServices();
        }

        ~MultiWindowLiveInspector()
        {
            GlobalInputListener?.UnregisterFromInputEvents();
        }

        public void Show()
        {
            ToolBarWindow.Show();
            if (InspectorStateManager.IsOverlayVisible)
            {
                OverlayWindow?.Show();
            }
            if (InspectorStateManager.IsPropertiesPanelVisible)
            {
                PropertiesWindow?.Show();
            }
            if (InspectorStateManager.IsTreeViewVisible)
            {
                TreeViewWindow?.Show();
            }
        }

        public void Close()
        {
            GlobalInputListener?.UnregisterFromInputEvents();

            OverlayWindow?.Close();
            PropertiesWindow?.Close();
            TreeViewWindow?.Close();
            ToolBarWindow.Close();
        }

        private void Hide()
        {
            OverlayWindow?.Hide();
            PropertiesWindow?.Hide();
            TreeViewWindow?.Hide();
            ToolBarWindow.Hide();
        }

        private void CreateWindows()
        {
            OverlayWindow = new OverlayWindow();
            ToolBarWindow = new ToolBarWindow();
            PropertiesWindow = new PropertiesWindow();
            TreeViewWindow = new TreeViewWindow();

            // Initially hide all the windows.
            PropertiesWindow.ShowInTaskbar = true;
            TreeViewWindow.ShowInTaskbar = true;
            ToolBarWindow.WindowState = WindowState.Minimized;
            PropertiesWindow.WindowState = WindowState.Minimized;
            TreeViewWindow.WindowState = WindowState.Minimized;

            // Wait for the windows to have rendered before showing them.
            ToolBarWindow.ContentRendered += WindowContentRendered;
            PropertiesWindow.ContentRendered += WindowContentRendered;
            TreeViewWindow.ContentRendered += WindowContentRendered;
        }

        private void InitializeServices()
        {
            IColorPickerService colorPickerService = new ColorPickerService();
            IDistanceOutlinesProvider distanceOutlinesProvider = new DistanceOutlinesProvider();
            IElementPropertiesProvider elementPropertiesProvider = new ElementPropertiesProvider();
            IElementProvider elementProvider = new BasicLiveElementProvider(elementPropertiesProvider);
            IFolderConfig folderConfig = new FolderConfig();

            OutlinesService = new OutlinesService(distanceOutlinesProvider, elementProvider);
            CoordinateConverter = new LiveCoordinateConverter(OverlayWindow);
            ScreenHelper = new ScreenHelper(OverlayWindow);

            InspectorStateManager = new InspectorStateManager();
            InspectorStateManager.IsOverlayVisibleChanged += OnIsOverlayVisibleChanged;
            InspectorStateManager.IsPropertiesPanelVisibleChanged += OnIsPropertiesPanelVisibleChanged;
            InspectorStateManager.IsTreeViewVisibleChanged += OnIsTreeViewVisibleChanged;

            WindowInputMaskingService inputMaskingService = new WindowInputMaskingService(CoordinateConverter);
            inputMaskingService.Ignore(ToolBarWindow);
            inputMaskingService.Ignore(PropertiesWindow);
            inputMaskingService.Ignore(TreeViewWindow);

            GlobalInputListener = new GlobalInputListener(inputMaskingService);

            IScreenshotService screenshotService = new ScreenshotService(Hide, Show);
            IUITreeService uiTreeService = new LiveUITreeService(elementPropertiesProvider, OutlinesService);
            ISnapshotService snapshotService =  new SnapshotService(screenshotService, uiTreeService, ScreenHelper, folderConfig);

            ColorPickerViewModel colorPickerViewModel = new ColorPickerViewModel(colorPickerService, GlobalInputListener);
            OverlayViewModel overlayViewModel = new OverlayViewModel(OverlayWindow.Dispatcher, OutlinesService, CoordinateConverter, ScreenHelper);
            PropertiesViewModel propertiesViewModel = new PropertiesViewModel(OutlinesService);
            ToolBarViewModel toolBarViewModel = new ToolBarViewModel(OutlinesService, screenshotService, snapshotService, folderConfig, CoordinateConverter, InspectorStateManager);
            UITreeViewModel uiTreeViewModel = new UITreeViewModel(TreeViewWindow.Dispatcher, OutlinesService, uiTreeService);

            var serviceContainer = ServiceContainer.Instance;
            serviceContainer.AddService(typeof(ColorPickerViewModel), colorPickerViewModel);
            serviceContainer.AddService(typeof(OverlayViewModel), overlayViewModel);
            serviceContainer.AddService(typeof(PropertiesViewModel), propertiesViewModel);
            serviceContainer.AddService(typeof(ToolBarViewModel), toolBarViewModel);
            serviceContainer.AddService(typeof(UITreeViewModel), uiTreeViewModel);

            HoverWatcher targetHoverWatcher = new HoverWatcher(TargetHoverDelayInMs);
            targetHoverWatcher.MouseHovered += OnMouseHovered;
            GlobalInputListener.MouseMoved += targetHoverWatcher.OnMouseMoved;
            GlobalInputListener.MouseDown += OnMouseDown;
        }

        private void WindowContentRendered(object sender, EventArgs e)
        {
            RenderedWindows.Add(sender as Window);
     
            if (RenderedWindows.Contains(ToolBarWindow)
             && RenderedWindows.Contains(PropertiesWindow)
             && RenderedWindows.Contains(TreeViewWindow))
            {
                OnWindowsReady();
            }
        }

        private void OnWindowsReady()
        {
            PositionWindows();

            // Show the windows, when they all have rendered.
            ToolBarWindow.WindowState = WindowState.Normal;
            PropertiesWindow.WindowState = WindowState.Normal;
            TreeViewWindow.WindowState = WindowState.Normal;
            PropertiesWindow.ShowInTaskbar = false;
            TreeViewWindow.ShowInTaskbar = false;

            HideWindowsFromUia();
            GlobalInputListener?.RegisterToInputEvents();
        }

        private void PositionWindows()
        {
            var displayRect = ScreenHelper.GetPrimaryDisplayRect();
            var localDisplayRect = CoordinateConverter.RectFromScreen(displayRect);

            // Position the Toolbar at the Top-Center of the screen.
            ToolBarWindow.Top = localDisplayRect.Top;
            ToolBarWindow.Left = localDisplayRect.Left + localDisplayRect.Width / 2 - ToolBarWindow.ActualWidth / 2;

            // Position the Properties window on the right side of the screen.
            const int TopOffset = 100;
            PropertiesWindow.Top = localDisplayRect.Top + TopOffset;
            PropertiesWindow.Left = localDisplayRect.Right - PropertiesWindow.ActualWidth;

            // Position the TreeView window on the left side of the screen.
            TreeViewWindow.Top = localDisplayRect.Top + TopOffset;
            TreeViewWindow.Left = localDisplayRect.Left;
        }

        private void HideWindowsFromUia()
        {
            foreach (Window window in App.Current.Windows)
            {
                if (window != ToolBarWindow && window != PropertiesWindow && window != TreeViewWindow)
                {
                    IntPtr hwnd = new WindowInteropHelper(window).EnsureHandle();
                    UiaWindowHelper.HideWindowFromUia(hwnd);
                }
            }
        }

        private void OnMouseHovered(System.Drawing.Point cursorPos)
        {
            OutlinesService.TargetElementAt(cursorPos);
        }

        private void OnMouseDown(System.Drawing.Point cursorPos)
        {
            OutlinesService.SelectElementAt(cursorPos);
        }

        private void OnIsOverlayVisibleChanged(bool isOverlayVisible)
        {
            if (isOverlayVisible)
            {
                HideWindowsFromUia();
                OverlayWindow?.Show();
            }
            else
            {
                OverlayWindow?.Hide();
            }
        }

        private void OnIsPropertiesPanelVisibleChanged(bool isPropertiesPanelVisible)
        {
            if (isPropertiesPanelVisible)
            {
                PropertiesWindow?.Show();
            }
            else
            {
                PropertiesWindow?.Hide();
            }
        }

        private void OnIsTreeViewVisibleChanged(bool isTreeViewVisible)
        {
            if (isTreeViewVisible)
            {
                TreeViewWindow?.Show();
            }
            else
            {
                TreeViewWindow?.Hide();
            }
        }
    }
}
