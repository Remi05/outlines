using System;
using System.Windows;
using System.Windows.Media;

namespace Outlines.App.Services
{
    public class TitlebarThemeManager
    {
        private ThemeManager ThemeManager { get; set; }
        private TitlebarHelper TitlebarHelper { get; set; }
        private IntPtr WindowToUpdate { get; set; }

        public TitlebarThemeManager(ThemeManager themeManager, TitlebarHelper titlebarHelper, IntPtr windowToUpdate)
        {
            ThemeManager = themeManager;
            TitlebarHelper = titlebarHelper;
            WindowToUpdate = windowToUpdate;

            UpdateTitlebarTheme();
            ThemeManager.ThemeChanged += UpdateTitlebarTheme;
        }

        private void UpdateTitlebarTheme()
        {
            Color titlebarBackgroundColor = (Color)Application.Current.Resources["ThemeTitlebarBackgroundColor"];
            TitlebarHelper.SetTitlebarBackgroundColor(WindowToUpdate, titlebarBackgroundColor);
        }
    }
}
