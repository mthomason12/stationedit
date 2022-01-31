using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;

namespace StationEdit.Things.Roots
{
    public class StructureFrame : StationStructure
    {

        public StructureFrame(string prefabName, XElement thing) : base(prefabName, thing)
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


            RectangleGeometry myRect = new RectangleGeometry();
            myRect.Rect = new Rect(0, 0, 40, 40);

            LineGeometry lineX = new LineGeometry(new Point(0, 0), new Point(40, 40));
            LineGeometry lineY = new LineGeometry(new Point(0, 40), new Point(40, 0));

            GeometryGroup geo = new GeometryGroup();
            geo.Children.Add(myRect);
            geo.Children.Add(lineX);
            geo.Children.Add(lineY);

            frame.Data = geo;

            return frame;
        }



    }
}
