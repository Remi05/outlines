using System;
using System.Windows;
using System.Windows.Media;

namespace Outlines.App.Services
{
    public class WindowEffectsManager
    {
        private ThemeManager ThemeManager { get; set; }
        private WindowEffectsHelper WindowEffectsHelper { get; set; }
        private IntPtr WindowToUpdate { get; set; }
        private bool IsMicaEnabled { get; set; } = true;

        public WindowEffectsManager(ThemeManager themeManager, WindowEffectsHelper windowEffectsHelper, IntPtr windowToUpdate)
        {
            ThemeManager = themeManager;
            WindowEffectsHelper = windowEffectsHelper;
            WindowToUpdate = windowToUpdate;

            WindowEffectsHelper.SetMicaEffect(WindowToUpdate, IsMicaEnabled);

            UpdateThemedWindowEffects();
            ThemeManager.ThemeChanged += UpdateThemedWindowEffects;
        }

        private void UpdateThemedWindowEffects()
        {
            if (IsMicaEnabled)
            {
                // Only set dark mode with Mica, the Titlebar color is already theme-aware.
                WindowEffectsHelper.SetShouldUseDarkMode(WindowToUpdate, !ThemeManager.IsLightTheme);
            }
            else
            {
                Color titlebarBackgroundColor = (Color)Application.Current.Resources["ThemeTitlebarBackgroundColor"];
                WindowEffectsHelper.SetTitlebarBackgroundColor(WindowToUpdate, titlebarBackgroundColor);
            }
        }
    }
}
