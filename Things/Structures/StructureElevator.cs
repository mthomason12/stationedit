using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace StationEdit.Things.Structures
{
    public class StructureElevator : StationStructure
    {

        RectangleGeometry myRect = new RectangleGeometry();
        RectangleGeometry myWindow = new RectangleGeometry();

        public StructureElevator(string prefabName, XElement thing) : base(prefabName, thing)
        {
            fill = System.Windows.Media.Brushes.LightGray;
            height = 80;
            width = 40;
        }

        protected override FrameworkElement GetUIElement()
        {
            Canvas myCanvas;
            myCanvas = new Canvas();

            System.Windows.Shapes.Path frame;
            frame = new System.Windows.Shapes.Path();

            frame.Stroke = System.Windows.Media.Brushes.Black;

            frame.Fill = fill;
            frame.StrokeThickness = 1;
            frame.HorizontalAlignment = HorizontalAlignment.Left;
            frame.VerticalAlignment = VerticalAlignment.Center;

            myRect.Rect = new Rect(0, 0, 40, 40);
            myWindow.Rect = new Rect(4, 4, 32, 32);

            Orientate(frame);

            CombinedGeometry geo = new CombinedGeometry();
            geo.Geometry1 = myRect;
            geo.Geometry2 = myWindow;
            geo.GeometryCombineMode = GeometryCombineMode.Exclude;
            frame.Data = geo;
            frame.Stretch = Stretch.None;
            frame.Height = height;
            frame.Width = width;

            Rectangle shader = new Rectangle();
            shader.Width = 32;
            shader.Height = 32;
            shader.Stroke = System.Windows.Media.Brushes.Transparent;
            shader.StrokeThickness = 0;
            shader.Fill = new SolidColorBrush(Color.FromArgb(16 ,0, 0, 0)); //this will layer, so the deeper the shaft, the blacker it is!

            myCanvas.Width = 40;
            myCanvas.Height = 80;
            myCanvas.Background = System.Windows.Media.Brushes.Transparent;
            myCanvas.Children.Add(frame);
            Canvas.SetLeft(frame, 0);
            Canvas.SetTop(frame, 0);
            myCanvas.Children.Add(shader);
            Canvas.SetLeft(shader, 4);
            Canvas.SetTop(shader, 4);
            myCanvas.Tag = this;

            return myCanvas;
        }



    }
}
