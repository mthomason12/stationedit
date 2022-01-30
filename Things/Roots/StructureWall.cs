using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace StationEdit.Things.Roots
{
    public class StructureWall : StructurePanel
    {
        public StructureWall(string prefabName, XElement thing) : base(prefabName, thing)
        {
        }

        /*
        protected override void Orientate(FrameworkElement myShape)
        {
            switch (orientation)
            {
                case Orientation.front:
                case Orientation.back:
                    myShape.Height = 10;
                    myShape.Width = 40;
                    break;

                case Orientation.left:
                case Orientation.right:
                    myShape.Height = 40;
                    myShape.Width = 10;
                    break;

                case Orientation.up:
                case Orientation.down:
                default:
                    myShape.Height = 40;
                    myShape.Width = 40;
                    break;
            }
        }*/


    }
}
