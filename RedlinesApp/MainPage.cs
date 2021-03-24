using System.Drawing;
using System.Windows.Forms;
using Redlines;

namespace RedlinesApp
{
    public partial class MainPage : Form
    {
        private RedlinesService RedlinesService { get; set; } = new RedlinesService();
        private ColorConfig ColorConfig { get; set; } = new ColorConfig();
        private DimensionsConfig DimensionsConfig { get; set; } = new DimensionsConfig();
        private bool ShouldPaintOverlay { get; set; } = true;


        protected override bool ShowWithoutActivation => true;

        public MainPage()
        {
            InitializeComponent();

            BackColor = Color.Magenta;
            TransparencyKey = BackColor;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ShowIcon = false;
            TopMost = true;

            // Force a redraw when the selected or target element changes.
            RedlinesService.SelectedElementChanged += new SelectedElementChangedHandler(Invalidate);
            RedlinesService.TargetElementChanged += new TargetElementChangedHandler(Invalidate);

            // Register to global mouse events.
            MouseMessageFilter mouseHandler = new MouseMessageFilter();
            mouseHandler.MouseDown += new MouseDownEventHandler(OnMouseDown);
            mouseHandler.MouseMoved += new MouseMovedEventHandler(OnMouseMoved);
            Application.AddMessageFilter(mouseHandler);
        }

        private void OnMouseDown()
        {
            RedlinesService.SelectElementAt(Helpers.DrawingPointToWindowsPoint(Cursor.Position));
        }

        private void OnMouseMoved()
        {
            RedlinesService.TargetElementAt(Helpers.DrawingPointToWindowsPoint(Cursor.Position));
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

            // We don't want to draw the target element outline if it is also the selected element.
            if (RedlinesService.TargetElementProperties != RedlinesService.SelectedElementProperties)
            {
                DrawElementOutline(e.Graphics, RedlinesService.TargetElementProperties, ColorConfig.TargetElementOutlineColor);
            }
            DrawElementOutline(e.Graphics, RedlinesService.SelectedElementProperties, ColorConfig.SelectedElementOutlineColor);
        }

        private void DrawDistanceOutline(Graphics graphics, DistanceOutline distanceOutline, Color color)
        {
            if (distanceOutline.Distance == 0)
            {
                return;
            }

            Point screenStartPoint = Helpers.WindowsPointToDrawingPoint(distanceOutline.StartPoint);
            Point screenEndPoint = Helpers.WindowsPointToDrawingPoint(distanceOutline.EndPoint);
            graphics.DrawLine(new Pen(color, 1), PointToClient(screenStartPoint), PointToClient(screenEndPoint));

            Rectangle distanceTextRect = GetDistanceTextRectangle(distanceOutline);
            DrawFilledRectWithText(graphics, distanceTextRect, distanceOutline.Distance.ToString(), color, ColorConfig.TextColor);
        }

        private void DrawElementOutline(Graphics graphics, ElementProperties elementProperties, Color color)
        {
            if (elementProperties == null)
            {
                return;
            }
            Rectangle outlineRect = Helpers.WindowsRectToDrawingRect(elementProperties.BoundingRect);
            outlineRect.Location = PointToClient(outlineRect.Location);
            graphics.DrawRectangle(new Pen(color, 2), outlineRect);

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
    }
}
