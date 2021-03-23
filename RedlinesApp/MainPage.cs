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

        private Color DistanceOutlinesColor { get; set; } = Color.Red;
        private Color SelectedElementOutlineColor { get; set; } = Color.Blue;
        private Color TargetElementOutlineColor { get; set; } = Color.Green;

        public MainPage()
        {
            InitializeComponent();
            BackColor = Color.Magenta;
            TransparencyKey = Color.Magenta;
        }

        private void MainPage_MouseMove(object sender, MouseEventArgs e)
        {
            Point screenPoint = PointToScreen(e.Location);
            RedlinesService.OnCursorMove(Helpers.DrawingPointToWindowsPoint(screenPoint));
        }

        private void MainPage_MouseDown(object sender, MouseEventArgs e)
        {
            Point screenPoint = PointToScreen(e.Location);
            RedlinesService.OnCursorDown(Helpers.DrawingPointToWindowsPoint(screenPoint));
        }

        private void MainPage_Paint(object sender, PaintEventArgs e)
        {
            foreach (var distanceOutline in RedlinesService.DistanceOutlines)
            {
                DrawDistanceOutline(e.Graphics, distanceOutline, DistanceOutlinesColor);
            }

            DrawElementOutline(e.Graphics, RedlinesService.SelectedElementProperties, SelectedElementOutlineColor);
            DrawElementOutline(e.Graphics, RedlinesService.TargetElementProperties, TargetElementOutlineColor);
        }

        private void DrawDistanceOutline(Graphics graphics, DistanceOutline distanceOutline, Color color)
        {
            if (distanceOutline.Distance == 0)
            {
                return;
            }

            Point screenStartPoint = Helpers.WindowsPointToDrawingPoint(distanceOutline.StartPoint);
            Point screenEndPoint = Helpers.WindowsPointToDrawingPoint(distanceOutline.EndPoint);
            graphics.DrawLine(new Pen(color, 2), PointToClient(screenStartPoint), PointToClient(screenEndPoint));
        }

        private void DrawElementOutline(Graphics graphics, ElementProperties elementProperties, Color color)
        {
            if (elementProperties == null)
            {
                return;
            }
            Rectangle rectangle = Helpers.WindowsRectToDrawingRect(elementProperties.BoundingRect);
            rectangle.Location = PointToClient(rectangle.Location);
            graphics.DrawRectangle(new Pen(color, 2), rectangle);
        }
    }
}
