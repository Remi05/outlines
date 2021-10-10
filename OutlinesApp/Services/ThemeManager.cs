using System;
using System.Windows;
using System.Windows.Media;
using Windows.UI.ViewManagement;

namespace OutlinesApp.Services
{
    public static class ColorHelpers
    {
        public static Color ToWpfColor(this Windows.UI.Color color)
        {
            return Color.FromArgb(color.A, color.R, color.G, color.B);
        }
    }

    public class ThemeManager
    {
        private UISettings UISettings { get; set; }
        private ResourceDictionary SystemColorsDictionary { get; set; } = new ResourceDictionary();
        private ResourceDictionary CurrentThemeDictionary { get; set; }
        private bool IsLightTheme { get; set; }

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
            IsLightTheme = UISettings.GetColorValue(UIColorType.Background) == Windows.UI.Colors.White;
            UpdateSystemColorsDictionary();
            UpdateThemeDictionary();
        }

        private void UpdateThemeDictionary()
        {
            string themeDictionaryUri = IsLightTheme ? "LightThemeResources.xaml" : "DarkThemeResources.xaml";
            var newThemeDictionary = Application.LoadComponent(new Uri(themeDictionaryUri, UriKind.RelativeOrAbsolute)) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Remove(CurrentThemeDictionary);
            CurrentThemeDictionary = newThemeDictionary;
            Application.Current.Resources.MergedDictionaries.Add(CurrentThemeDictionary);
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
