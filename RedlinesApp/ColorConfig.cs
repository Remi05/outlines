using System.Drawing;

namespace RedlinesApp
{
    public class ColorConfig
    {
        public class DefaultColors
        {
            public static Color OrangeRed => Color.FromArgb(242, 72, 34); // #F24822
            public static Color LightBlue => Color.FromArgb(24, 160, 251); // #18A0FB

            public static Color DefaultDistanceOutlineColor = OrangeRed;
            public static Color DefaultSelectedElementOutlineColor = LightBlue;
            public static Color DefaultTargetElementOutlineColor = OrangeRed;
            public static Color DefaultTextColor = Color.White;
        }

        public Color DistanceOutlineColor { get; set; } = DefaultColors.DefaultDistanceOutlineColor;
        public Color SelectedElementOutlineColor { get; set; } = DefaultColors.DefaultSelectedElementOutlineColor;
        public Color TargetElementOutlineColor { get; set; } = DefaultColors.DefaultTargetElementOutlineColor;
        public Color TextColor { get; set; } = DefaultColors.DefaultTextColor;
    }
}
