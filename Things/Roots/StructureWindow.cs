using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using System.Diagnostics;

namespace StationEdit.Things.Roots
{
    public class StructureWindow : StationStructure
    {

        RectangleGeometry myRect = new RectangleGeometry();
        RectangleGeometry myWindow = new RectangleGeometry();

        public StructureWindow(string prefabName, XElement thing) : base(prefabName, thing)
        {
            fill = System.Windows.Media.Brushes.Gray;
            height = 40;
            width = 40;
        }

        protected override FrameworkElement GetUIElement()
        {
            System.Windows.Shapes.Path frame;
            frame = new System.Windows.Shapes.Path();

            frame.Stroke = System.Windows.Media.Brushes.Black;

            frame.Fill = fill;
            frame.StrokeThickness = 1;
            frame.HorizontalAlignment = HorizontalAlignment.Left;
            frame.VerticalAlignment = VerticalAlignment.Center;
            frame.Tag = this;

            myRect.Rect = new Rect(0, 0, 40, 40);
            myWindow.Rect = new Rect(5, 5, 30, 30);

            Orientate(frame);

            CombinedGeometry geo = new CombinedGeometry();
            geo.Geometry1 = myRect;
            geo.Geometry2 = myWindow;
            geo.GeometryCombineMode = GeometryCombineMode.Exclude;
            frame.Data = geo;
            frame.Stretch = Stretch.Fill;
            frame.Height = height;
            frame.Width = width;

            return frame;
        }


        protected override void Orientate(FrameworkElement myShape)
        {
            myShape.Height = 40;
            myShape.Width = 40;

            switch (orientation)
            {
                case Orientation.front:
                case Orientation.back:
                    height = 8;
                    width = 40;
                    myWindow.Rect = new Rect(4, 2, 32, 38);
                    break;

                case Orientation.left:
                case Orientation.right:
                    height = 40;
                    width = 8;
                    myWindow.Rect = new Rect(2, 4, 38, 32);
                    break;

                case Orientation.up:
                case Orientation.down:
                default:
                    height = 40;
                    width = 40;
                    break;
            }
        }





    }
}
