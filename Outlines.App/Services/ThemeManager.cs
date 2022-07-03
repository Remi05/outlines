using System;
using System.Windows;
using System.Windows.Media;
using Windows.UI.ViewManagement;

namespace Outlines.App.Services
{
    public static class ColorHelpers
    {
        public static Color ToWpfColor(this Windows.UI.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }

    public delegate void ThemeChangedEventHandler();

    public class ThemeManager
    {
        private UISettings UISettings { get; set; }
        private ResourceDictionary SystemColorsDictionary { get; set; } = new ResourceDictionary();
        private ResourceDictionary CurrentThemeDictionary { get; set; }
        public bool IsLightTheme { get; set; }

        public event ThemeChangedEventHandler ThemeChanged;

        public ThemeManager()
        {
            UISettings = new UISettings();
            UISettings.ColorValuesChanged += OnColorValuesChanged;
            UpdateResourceDictionaries();
        }
        private void OnColorValuesChanged(UISettings sender, object args)
        {
            UpdateResourceDictionaries();
        }

        private void UpdateResourceDictionaries()
        {            
            UpdateSystemColorsDictionary();
            UpdateThemeDictionary();
        }

        private void UpdateThemeDictionary()
        {
            var isLightTheme = IsCurrentThemeLightTheme();
            if (isLightTheme != IsLightTheme || CurrentThemeDictionary == null)
            {
                IsLightTheme = isLightTheme;

                string themeDictionaryUri = IsLightTheme ? "Resources/LightThemeResources.xaml" : "Resources/DarkThemeResources.xaml";
                var newThemeDictionary = Application.LoadComponent(new Uri(themeDictionaryUri, UriKind.Relative)) as ResourceDictionary;
                Application.Current.Resources.MergedDictionaries.Remove(CurrentThemeDictionary);
                CurrentThemeDictionary = newThemeDictionary;
                Application.Current.Resources.MergedDictionaries.Add(CurrentThemeDictionary);

                ThemeChanged?.Invoke();
            }
        }

        private bool IsCurrentThemeLightTheme()
        {
            return UISettings.GetColorValue(UIColorType.Background) == Windows.UI.Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
        }

        private void UpdateSystemColorsDictionary()
        {
            Application.Current.Resources.MergedDictionaries.Remove(SystemColorsDictionary);
            UpdateSystemColorResource(UIColorType.Accent, "AccentColor");
            UpdateSystemColorResource(UIColorType.AccentLight1, "AccentLight1Color");
            UpdateSystemColorResource(UIColorType.AccentLight2, "AccentLight2Color");
            UpdateSystemColorResource(UIColorType.AccentLight3, "AccentLight3Color");
            UpdateSystemColorResource(UIColorType.AccentDark1, "AccentDark1Color");
            UpdateSystemColorResource(UIColorType.AccentDark2, "AccentDark2Color");
            UpdateSystemColorResource(UIColorType.AccentDark3, "AccentDark3Color");
            UpdateSystemColorResource(UIColorType.Background, "BackgroundColor");
            UpdateSystemColorResource(UIColorType.Foreground, "ForegroundColor");
            Application.Current.Resources.MergedDictionaries.Add(SystemColorsDictionary);
        }

        private void UpdateSystemColorResource(UIColorType uiColorType, string colorKey)
        {
            var color = UISettings.GetColorValue(uiColorType);
            SystemColorsDictionary[colorKey] = color.ToWpfColor();
        }
    }
}
