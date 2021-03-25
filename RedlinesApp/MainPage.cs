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

            BackColor = Color.Magenta;
            TransparencyKey = BackColor;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ShowInTaskbar = false;
            ShowIcon = false;
            TopMost = true;

            // Force a redraw when the selected or target element changes.
            RedlinesService.SelectedElementChanged += Invalidate;
            RedlinesService.TargetElementChanged += Invalidate;

            // Register to global mouse events.
            GlobalInputListener = new GlobalInputListener();
            GlobalInputListener.MouseDown += OnMouseDown;
            GlobalInputListener.MouseMoved += OnMouseMoved;
            GlobalInputListener.RegisterToMouseEvents();

            // Set the target element after hovering for 0.2s.
            TargetTimer = new System.Timers.Timer();
            TargetTimer.Interval = 200;
            TargetTimer.AutoReset = false;
            TargetTimer.Elapsed += (_, __) => OnTargetTimeElapsed();
        }

        private void OnMouseDown()
        {
            Hide();
            RedlinesService.SelectElementAt(Helpers.DrawingPointToWindowsPoint(Cursor.Position));
            Show();
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
            if (distanceOutline.IsGap)
            {
                linePen.DashStyle = DashStyle.Custom;
                linePen.DashPattern = new float[] { DimensionsConfig.DashLength, DimensionsConfig.DashLength };
            }

            Point screenStartPoint = Helpers.WindowsPointToDrawingPoint(distanceOutline.StartPoint);
            Point screenEndPoint = Helpers.WindowsPointToDrawingPoint(distanceOutline.EndPoint);
            graphics.DrawLine(linePen, PointToClient(screenStartPoint), PointToClient(screenEndPoint));

            if (!distanceOutline.IsGap)
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
            Size offset = distanceOutline.IsVertical ? new Size(DimensionsConfig.TextRectangleOffset, 0) : new Size(0, DimensionsConfig.TextRectangleOffset);
            Point rectPos = Point.Add(Helpers.WindowsPointToDrawingPoint(distanceOutline.MidPoint), offset);
            return new Rectangle(rectPos, DimensionsConfig.DistanceRectangleSize);
        }

        private Rectangle GetDimensionsTextRectangle(System.Windows.Rect outlineRect)
        {
            Size offset = new Size(-DimensionsConfig.DimensionsRectangleSize.Width / 2, DimensionsConfig.TextRectangleOffset);
            System.Windows.Point bottomCenter = System.Windows.Point.Add(outlineRect.BottomLeft, System.Windows.Point.Subtract(outlineRect.BottomRight, outlineRect.BottomLeft) / 2);
            Point rectPos = Point.Add(Helpers.WindowsPointToDrawingPoint(bottomCenter), offset);
            return new Rectangle(rectPos, DimensionsConfig.DimensionsRectangleSize);
        }

        private void toggleOverlayToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ToggleShoudlPaintOverlay();
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }
    }
}
