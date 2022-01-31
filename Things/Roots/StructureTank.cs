using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace StationEdit.Things.Roots
{
    public class StructureTank : StructureItem
    {
        public StructureTank(string prefabName, XElement thing) : base(prefabName, thing)
        {
            fill = System.Windows.Media.Brushes.Gray;
            height = 38;
            width = 38;
        }

        protected override FrameworkElement GetUIElement()
        {
            System.Windows.Shapes.Ellipse myShape;
            myShape = new System.Windows.Shapes.Ellipse();
            myShape.Stroke = System.Windows.Media.Brushes.Black;
            myShape.Fill = fill;
            myShape.HorizontalAlignment = HorizontalAlignment.Left;
            myShape.VerticalAlignment = VerticalAlignment.Center;

            myShape.Tag = this;

            myShape.Width = width;
            myShape.Height = height;

            return myShape;
        }
    }
}
