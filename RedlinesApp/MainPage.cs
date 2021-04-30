using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Redlines;

namespace RedlinesApp
{
    public partial class MainPage : Form
    {
        private RedlinesService RedlinesService { get; set; } = new RedlinesService();
        private ColorConfig ColorConfig { get; set; } = new ColorConfig();
        private DimensionsConfig DimensionsConfig { get; set; } = new DimensionsConfig();
        private GlobalInputListener GlobalInputListener { get; set; }
        private ColorPicker ColorPicker { get; set; } = new ColorPicker();
        private System.Timers.Timer TargetTimer { get; set; }
        private bool ShouldPaintOverlay { get; set; } = true;

        protected override bool ShowWithoutActivation => true;

        public MainPage()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            ShowIcon = false;
            TopMost = true;

            MakeWindowTransparent();

            ScreenHelper.CoverAllDisplays(this);

            // Force a redraw when the selected or target element changes.
            RedlinesService.SelectedElementChanged += Invalidate;
            RedlinesService.TargetElementChanged += Invalidate;

            // Register to global mouse events.
            GlobalInputListener = new GlobalInputListener();
            GlobalInputListener.MouseDown += OnMouseDown;
            GlobalInputListener.MouseMoved += OnMouseMoved;
            GlobalInputListener.KeyDown += OnKeyDown;
            GlobalInputListener.KeyUp += OnKeyUp;
            GlobalInputListener.RegisterToInputEvents();

            // Set the target element after hovering for 0.2s.
            TargetTimer = new System.Timers.Timer();
            TargetTimer.Interval = 500;
            TargetTimer.AutoReset = false;
            TargetTimer.Elapsed += (_, __) => OnTargetTimeElapsed();
        }

        private void OnMouseDown()
        {
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl))
            {
                Hide();
                RedlinesService.SelectElementAt(Helpers.DrawingPointToWindowsPoint(Cursor.Position));
                Show();
            }
        }

        private void OnMouseMoved()
        {
            UpdateColorPicker();
            TargetTimer.Stop();
            TargetTimer.Start();
        }

        private void UpdateColorPicker()
        {
            Color curColor = ColorPicker.GetColorAt(Cursor.Position);
            currentColorPanel.BackColor = curColor;
            currentColorRgbValueLabel.Text = $"({curColor.R}, {curColor.G}, {curColor.B})";
            currentColorHexValueLabel.Text = $"#{curColor.R.ToString("X2")}{curColor.G.ToString("X2")}{curColor.B.ToString("X2")}";
        }

        private void OnKeyDown()
        {
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl))
            {
                MakeWindowSemiTransparent();
            }
        }

        private void OnKeyUp()
        {
            if (System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl))
            {
                MakeWindowTransparent();
            }
        }

        private delegate void TargetElementDelegate();
        private void OnTargetTimeElapsed()
        {
            Invoke(new TargetElementDelegate(() => {
                Hide();
                RedlinesService.TargetElementAt(Helpers.DrawingPointToWindowsPoint(Cursor.Position));
                Show();
            }));
        }

        private void ToggleShoudlPaintOverlay()                       
        {
            ShouldPaintOverlay = !ShouldPaintOverlay;
            if (!ShouldPaintOverlay)
            {
                Invalidate();
            }
        }

        private void MakeWindowTransparent()
        {
            BackColor = Color.Magenta;
            TransparencyKey = BackColor;
            Opacity = 1.0;
        }

        private void MakeWindowSemiTransparent()
        {
            BackColor = Color.Black;
            Opacity = 0.5;
        }

        private void MainPage_Paint(object sender, PaintEventArgs e)
        {
            if (!ShouldPaintOverlay)
            {
                return;
            }

            foreach (var distanceOutline in RedlinesService.DistanceOutlines)
            {
                DrawDistanceOutline(e.Graphics, distanceOutline, ColorConfig.DistanceOutlineColor);
            }

            DrawElementOutline(e.Graphics, RedlinesService.SelectedElementProperties, ColorConfig.SelectedElementOutlineColor);
            UpdateSelectedElementProperties();
            UpdateSelectedTextProperties();

            // We don't want to draw the target element outline if it is also the selected element.
            if (RedlinesService.TargetElementProperties != RedlinesService.SelectedElementProperties)
            {
                DrawElementOutline(e.Graphics, RedlinesService.TargetElementProperties, ColorConfig.TargetElementOutlineColor);
            }
        }

        private void UpdateSelectedElementProperties()
        {
            if (RedlinesService.SelectedElementProperties == null)
            {
                return;
            }

            nameValueLabel.Text = RedlinesService.SelectedElementProperties.Name;

            var selectedElementRect = RedlinesService.SelectedElementProperties.BoundingRect;
            widthValueLabel.Text = selectedElementRect.Width.ToString() + "px";
            heightValueLabel.Text = selectedElementRect.Height.ToString() + "px";
            topValueLabel.Text = selectedElementRect.Top.ToString() + "px";
            leftValueLabel.Text = selectedElementRect.Left.ToString() + "px";
        }

        private void UpdateSelectedTextProperties()
        {
            if (RedlinesService.SelectedTextProperties != null)
            {
                fontNameValueLabel.Text = RedlinesService.SelectedTextProperties.FontName;
                fontSizeValueLabel.Text = RedlinesService.SelectedTextProperties.FontSize + "px";
                fontWeightValueLabel.Text = RedlinesService.SelectedTextProperties.FontWeight;           
            }
            else
            {
                fontNameValueLabel.Text = "";
                fontSizeValueLabel.Text = "";
                fontWeightValueLabel.Text = "";
            }
        }

        private void DrawDistanceOutline(Graphics graphics, DistanceOutline distanceOutline, Color color)
        {
            if (distanceOutline.Distance == 0)
            {
                return;
            }

            Pen linePen = new Pen(color, DimensionsConfig.DistanceOutlineWidth);
            if (distanceOutline.IsDashedLine)
            {
                linePen.DashStyle = DashStyle.Custom;
                linePen.DashPattern = new float[] { DimensionsConfig.DashLength, DimensionsConfig.DashLength };
            }

            Point screenStartPoint = Helpers.WindowsPointToDrawingPoint(distanceOutline.StartPoint);
            Point screenEndPoint = Helpers.WindowsPointToDrawingPoint(distanceOutline.EndPoint);
            graphics.DrawLine(linePen, PointToClient(screenStartPoint), PointToClient(screenEndPoint));

            if (!distanceOutline.IsDashedLine)
            {
                Rectangle distanceTextRect = GetDistanceTextRectangle(distanceOutline);
                DrawFilledRectWithText(graphics, distanceTextRect, distanceOutline.Distance.ToString(), color, ColorConfig.TextColor);
            }
        }

        private void DrawElementOutline(Graphics graphics, ElementProperties elementProperties, Color color)
        {
            if (elementProperties == null)
            {
                return;
            }
            Rectangle outlineRect = Helpers.WindowsRectToDrawingRect(elementProperties.BoundingRect);
            outlineRect.Location = PointToClient(outlineRect.Location);
            graphics.DrawRectangle(new Pen(color, DimensionsConfig.ElementOutlineWidth), outlineRect);

            Rectangle dimensionsTextRect = GetDimensionsTextRectangle(elementProperties.BoundingRect);
            string dimensionsText = $"{elementProperties.BoundingRect.Width} x {elementProperties.BoundingRect.Height}";
            DrawFilledRectWithText(graphics, dimensionsTextRect, dimensionsText, color, ColorConfig.TextColor);
        }

        private void DrawFilledRectWithText(Graphics graphics, Rectangle rect, string text, Color fillColor, Color textColor)
        {
            Brush rectBrush = new SolidBrush(fillColor);
            graphics.FillRectangle(rectBrush, rect);

            Brush textBrush = new SolidBrush(textColor);
            Font font = SystemFonts.DefaultFont;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
            graphics.DrawString(text, font, textBrush, rect, stringFormat);
        }

        private Rectangle GetDistanceTextRectangle(DistanceOutline distanceOutline)
        {
            Point midPoint = Helpers.WindowsPointToDrawingPoint(distanceOutline.MidPoint);

            // Default to a centered rectangle to the right or below the line.
            Size offset = distanceOutline.IsVertical ? new Size(DimensionsConfig.TextRectangleOffset, 0) : new Size(0, DimensionsConfig.TextRectangleOffset);
            Point rectPos = Point.Add(midPoint, offset);
            Rectangle textRect = new Rectangle(rectPos, DimensionsConfig.DistanceRectangleSize);

            Rectangle monitorRect = Screen.FromPoint(midPoint).Bounds;
            if (!monitorRect.Contains(textRect))
            {
                // If the text is outside the screen when shown to the right or below, try on the left or above.
                offset = distanceOutline.IsVertical ? new Size(-DimensionsConfig.TextRectangleOffset - DimensionsConfig.DistanceRectangleSize.Width, 0)
                                                    : new Size(0, -DimensionsConfig.TextRectangleOffset - DimensionsConfig.DistanceRectangleSize.Height);
                rectPos = Point.Add(midPoint, offset);
                textRect = new Rectangle(rectPos, DimensionsConfig.DistanceRectangleSize);
            }

            return textRect;
        }

        private Rectangle GetDimensionsTextRectangle(System.Windows.Rect outlineRect)
        {
            Rectangle monitorRect = Screen.FromRectangle(Helpers.WindowsRectToDrawingRect(outlineRect)).Bounds;

            // Default to a centered rectangle below the outline.
            Size offset = new Size(-DimensionsConfig.DimensionsRectangleSize.Width / 2, DimensionsConfig.TextRectangleOffset);
            System.Windows.Point bottomCenter = System.Windows.Point.Add(outlineRect.BottomLeft, System.Windows.Point.Subtract(outlineRect.BottomRight, outlineRect.BottomLeft) / 2);
            Point rectPos = Point.Add(Helpers.WindowsPointToDrawingPoint(bottomCenter), offset);
            Rectangle textRect = new Rectangle(rectPos, DimensionsConfig.DimensionsRectangleSize);

            if (!monitorRect.Contains(textRect))
            {
                // If the text is outside the screen when shown below, try above the outline.
                offset.Height = -DimensionsConfig.TextRectangleOffset - DimensionsConfig.DimensionsRectangleSize.Height;
                System.Windows.Point topCenter = System.Windows.Point.Add(outlineRect.TopLeft, System.Windows.Point.Subtract(outlineRect.TopRight, outlineRect.TopLeft) / 2);
                rectPos = Point.Add(Helpers.WindowsPointToDrawingPoint(topCenter), offset);
                textRect = new Rectangle(rectPos, DimensionsConfig.DimensionsRectangleSize);

                if (!monitorRect.Contains(textRect))
                {
                    // Fallback to a centered rectangle at the bottom of the outline, but inside of it, if it can't be shown above or below.
                    rectPos = Point.Add(Helpers.WindowsPointToDrawingPoint(bottomCenter), offset);
                    textRect = new Rectangle(rectPos, DimensionsConfig.DimensionsRectangleSize);
                }
            }

            return textRect;
        }

        private void toggleOverlayToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ToggleShoudlPaintOverlay();
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void togglePropertiesPaneToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            propertiesPanel.Visible = !propertiesPanel.Visible;
        }
    }
}
