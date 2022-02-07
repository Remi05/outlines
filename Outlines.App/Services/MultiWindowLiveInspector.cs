using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Outlines.App.ViewModels;
using Outlines.Core;
using Outlines.Inspection;
using Outlines.Inspection.NetCore;

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
        private IgnorableWindowsSource IgnorableWindowsSource { get; set; } = new IgnorableWindowsSource();

        private ToolBarWindow ToolBarWindow { get; set; }
        private OverlayWindow OverlayWindow { get; set; }
        private PropertiesWindow PropertiesWindow { get; set; }
        private TreeViewWindow TreeViewWindow { get; set; }
        private BackdropWindow BackdropWindow { get; set; }

        private ISet<Window> RenderedWindows { get; set; } = new HashSet<Window>();

        private bool IsClosing { get; set; } = false;

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
            if (InspectorStateManager.IsBackdropVisible)
            {
                BackdropWindow?.Show();
            }
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
            if (IsClosing)
            {
                return;
            }

            IsClosing = true;
            GlobalInputListener?.UnregisterFromInputEvents();

            BackdropWindow?.Close();
            OverlayWindow?.Close();
            PropertiesWindow?.Close();
            TreeViewWindow?.Close();
            ToolBarWindow?.Close();
        }

        private void Hide()
        {
            BackdropWindow?.Hide();
            OverlayWindow?.Hide();
            PropertiesWindow?.Hide();
            TreeViewWindow?.Hide();
            ToolBarWindow.Hide();
        }

        private void CreateWindows()
        {
            BackdropWindow = new BackdropWindow();
            OverlayWindow = new OverlayWindow();
            ToolBarWindow = new ToolBarWindow();
            PropertiesWindow = new PropertiesWindow();
            TreeViewWindow = new TreeViewWindow();

            BackdropWindow.MouseDown += OnBackdropWindowClicked;

            // Shutdown the app if the ToolBar window is closed.
            ToolBarWindow.Closing += (_, __) =>
            {
                if (!IsClosing)
                {
                    App.Current.Shutdown(0);
                }
            };

            // Ignore all the windows in the TreeView and Snapshots.
            IgnorableWindowsSource.IgnoreWindow(BackdropWindow);
            IgnorableWindowsSource.IgnoreWindow(OverlayWindow);
            IgnorableWindowsSource.IgnoreWindow(ToolBarWindow);
            IgnorableWindowsSource.IgnoreWindow(PropertiesWindow);
            IgnorableWindowsSource.IgnoreWindow(TreeViewWindow);

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

            ScreenHelper = new ScreenHelper(OverlayWindow);
            CoordinateConverter = new CachedCoordinateConverter(ScreenHelper.GetDisplayScaleFactor(), new System.Drawing.Point(0, 0));
            OutlinesService = new OutlinesService(distanceOutlinesProvider, elementProvider);
            OutlinesService.TargetElementChanged += OnTargetElementChanged;

            InspectorStateManager = new InspectorStateManager();
            InspectorStateManager.IsOverlayVisibleChanged += OnIsOverlayVisibleChanged;
            InspectorStateManager.IsPropertiesPanelVisibleChanged += OnIsPropertiesPanelVisibleChanged;
            InspectorStateManager.IsTreeViewVisibleChanged += OnIsTreeViewVisibleChanged;
            InspectorStateManager.IsBackdropVisibleChanged += OnIsBackdropVisibleChanged;

            WindowInputMaskingService inputMaskingService = new WindowInputMaskingService(CoordinateConverter);
            inputMaskingService.Ignore(BackdropWindow);
            inputMaskingService.Ignore(ToolBarWindow);
            inputMaskingService.Ignore(PropertiesWindow);
            inputMaskingService.Ignore(TreeViewWindow);

            GlobalInputListener = new GlobalInputListener(inputMaskingService);

            IScreenshotService screenshotService = new ScreenshotService(Hide, Show);
            IUITreeService uiTreeService = new LiveUITreeService(elementPropertiesProvider, OutlinesService, IgnorableWindowsSource);
            ISnapshotService snapshotService =  new SnapshotService(screenshotService, uiTreeService, ScreenHelper, folderConfig);
            ICoordinateConverter dpiIndependentCoordinateConverter = new DpiIndependentCoordinateConverter();

            ColorPickerViewModel colorPickerViewModel = new ColorPickerViewModel(colorPickerService, GlobalInputListener);
            OverlayViewModel overlayViewModel = new OverlayViewModel(OverlayWindow.Dispatcher, OutlinesService, dpiIndependentCoordinateConverter, ScreenHelper);
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
            GlobalInputListener.KeyDown += OnKeyDown;
            GlobalInputListener.KeyUp += OnKeyUp;
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
            foreach (Window window in Application.Current.Windows)
            {
                if (window != ToolBarWindow && window != PropertiesWindow && window != TreeViewWindow && window != BackdropWindow)
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

        private void OnBackdropWindowClicked(object sender, MouseButtonEventArgs e)
        {
            if (OutlinesService.TargetElementProperties != null)
            {
                OutlinesService.SelectElementWithProperties(OutlinesService.TargetElementProperties);
            }
        }
        private void OnIsOverlayVisibleChanged(bool isOverlayVisible)
        {
            if (isOverlayVisible)
            {
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

        private void OnIsBackdropVisibleChanged(bool isBackdropVisible)
        {
            if (isBackdropVisible && OutlinesService.TargetElementProperties != null)
            {
                UpdateBackdropWindowBounds();

                BackdropWindow?.Show();

                // Show the backdrop window below the overlay window.
                IntPtr backdropWindowHandle = new WindowInteropHelper(BackdropWindow).EnsureHandle();
                IntPtr overlayWindowHandle = new WindowInteropHelper(OverlayWindow).EnsureHandle();
                var flags = NativeWindowService.SetWindowPosFlags.SWP_NOSIZE | NativeWindowService.SetWindowPosFlags.SWP_NOMOVE | NativeWindowService.SetWindowPosFlags.SWP_NOACTIVATE;
                NativeWindowService.SetWindowPos(backdropWindowHandle, overlayWindowHandle, 0, 0, 0, 0, flags);
            }
            else
            {
                BackdropWindow?.Hide();
            }
        }

        private void UpdateBackdropWindowBounds()
        {
            if (OutlinesService.TargetElementProperties != null)
            {
                var localElementRect = CoordinateConverter.RectFromScreen(OutlinesService.TargetElementProperties.BoundingRect);
                BackdropWindow.Dispatcher.Invoke(() =>
                {
                    BackdropWindow.Top = localElementRect.Top;
                    BackdropWindow.Left = localElementRect.Left;
                    BackdropWindow.Width = localElementRect.Width;
                    BackdropWindow.Height = localElementRect.Height;
                });
            }
        }

        private void OnTargetElementChanged()
        {
            if (OutlinesService.TargetElementProperties != null)
            {
                UpdateBackdropWindowBounds();
            }
            else
            {
                BackdropWindow?.Hide();
            }
        }

        private void OnKeyDown(int vkCode)
        {
            Key key = KeyInterop.KeyFromVirtualKey(vkCode);
            if (key == Key.LeftCtrl)
            {
                InspectorStateManager.IsBackdropVisible = true;
            }
        }

        private void OnKeyUp(int vkCode)
        {
            Key key = KeyInterop.KeyFromVirtualKey(vkCode);
            if (key == Key.LeftCtrl)
            {
                InspectorStateManager.IsBackdropVisible = false;
            }
        }
    }
}
