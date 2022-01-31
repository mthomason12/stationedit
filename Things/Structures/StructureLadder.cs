using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Xml.Linq;
using System.Diagnostics;

namespace StationEdit.Things.Structures
{
    public class StructureLadder : StationStructure
    {

        public StructureLadder(string prefabName, XElement thing) : base(prefabName, thing)
        {
            fill = System.Windows.Media.Brushes.Orange;
        }

        protected override FrameworkElement GetUIElement()
        {
            System.Windows.Shapes.Rectangle myShape;
            myShape = new System.Windows.Shapes.Rectangle();
            myShape.Stroke = System.Windows.Media.Brushes.Black;
            myShape.Fill = fill;
            myShape.HorizontalAlignment = HorizontalAlignment.Left;
            myShape.VerticalAlignment = VerticalAlignment.Center;

            switch (orientation)
            {
                case Orientation.front:
                case Orientation.back:
                    myShape.Height = 10;
                    myShape.Width = 20;
                    break;

                case Orientation.left:
                case Orientation.right:
                    myShape.Height = 20;
                    myShape.Width = 10;
                    break;

                default:
                    myShape.Height = 20;
                    myShape.Width = 20;
                    break;
            }

            //Debug.Write("X: "+rotx+" Y: "+roty+" Z: "+rotz+" = "+orientation+"\r\n");

            myShape.Tag = this;

            height = (int)myShape.Height;
            width = (int)myShape.Width;
            
            return myShape;
        }



    }
}
