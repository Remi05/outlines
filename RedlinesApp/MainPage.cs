using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Redlines;

namespace RedlinesApp
{
    public partial class MainPage : Form
    {
        private RedlinesService RedlinesService { get; set; } = new RedlinesService();

        private Color DistanceOutlinesColor { get; set; } = Color.FromArgb(242, 72, 34);
        private Color SelectedElementOutlineColor { get; set; } = Color.FromArgb(24, 160, 251);
        private Color TargetElementOutlineColor { get; set; } = Color.FromArgb(242, 72, 34);

        protected override bool ShowWithoutActivation => true;

        public MainPage()
        {
            // Make the app DPI-aware to get correct dimensions.
            Application.EnableVisualStyles();

            MouseMessageFilter mouseHandler = new MouseMessageFilter();
            mouseHandler.MouseDown += new MouseDownEventHandler(OnMouseDown);
            mouseHandler.MouseMoved += new MouseMovedEventHandler(OnMouseMoved);
            Application.AddMessageFilter(mouseHandler);

            InitializeComponent();

            BackColor = Color.Magenta;
            TransparencyKey = BackColor;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ShowIcon = false;
            TopMost = true;

            RedlinesService.SelectedElementChanged += new SelectedElementChangedHandler(Redraw);
            RedlinesService.TargetElementChanged += new TargetElementChangedHandler(Redraw);
        }

        private void OnMouseDown()
        {
            Invalidate();
            RedlinesService.OnCursorDown(Helpers.DrawingPointToWindowsPoint(Cursor.Position));
        }

        private void OnMouseMoved()
        {
            RedlinesService.OnCursorMove(Helpers.DrawingPointToWindowsPoint(Cursor.Position));
        }

        private void Redraw()
        {
            using (Graphics graphics = CreateGraphics())
            {
                foreach (var distanceOutline in RedlinesService.DistanceOutlines)
                {
                    DrawDistanceOutline(graphics, distanceOutline, DistanceOutlinesColor);
                }

                DrawElementOutline(graphics, RedlinesService.SelectedElementProperties, SelectedElementOutlineColor);
                DrawElementOutline(graphics, RedlinesService.TargetElementProperties, TargetElementOutlineColor);
            }
        }

        private void MainPage_Paint(object sender, PaintEventArgs e)
        {
            //    foreach (var distanceOutline in RedlinesService.DistanceOutlines)
            //    {
            //        DrawDistanceOutline(e.Graphics, distanceOutline, DistanceOutlinesColor);
            //    }

            //    DrawElementOutline(e.Graphics, RedlinesService.SelectedElementProperties, SelectedElementOutlineColor);
            //    DrawElementOutline(e.Graphics, RedlinesService.TargetElementProperties, TargetElementOutlineColor);
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

            Rectangle rect = Helpers.GetTextRectangleForDistanceOutline(distanceOutline);
            DrawFilledRectWithText(graphics, rect, distanceOutline.Distance.ToString(), color, Color.White);
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

            Rectangle sizeRect = Helpers.GetTextRectangleForOutlineRect(elementProperties.BoundingRect);
            string sizeText = $"{elementProperties.BoundingRect.Width} x {elementProperties.BoundingRect.Height}";
            DrawFilledRectWithText(graphics, sizeRect, sizeText, color, Color.White);
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
    }
}
