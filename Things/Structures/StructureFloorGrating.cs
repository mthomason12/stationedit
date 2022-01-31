using StationEdit.Things.Roots;
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
    public class StructureFloorGrating : StructurePanel
    {
        public StructureFloorGrating(string prefabName, XElement thing) : base(prefabName, thing)
        {
            fill = System.Windows.Media.Brushes.DarkGray;
        }

        protected override FrameworkElement GetUIElement()
        {
            //make a nice grating brush
            VisualBrush myBrush = new VisualBrush();

            Grid myGrid = new Grid();
            myGrid.Background = fill;
            myGrid.Width = 6;
            myGrid.Height = 6;
            myGrid.Children.Add(new Line() { X1 = 0, X2 = 0, Y1 = 0, Y2 = 6, Stroke = System.Windows.Media.Brushes.Black, StrokeThickness = 2 });

            myBrush.Visual = myGrid;
            myBrush.TileMode = TileMode.Tile;
            myBrush.Viewport = new System.Windows.Rect(0, 0, 6, 6);
            myBrush.ViewportUnits = BrushMappingMode.Absolute;
            myBrush.Viewbox = new System.Windows.Rect(0, 0, 6, 6);
            myBrush.ViewboxUnits = BrushMappingMode.Absolute;
            fill = myBrush;

            return base.GetUIElement();
        }


    }
}
