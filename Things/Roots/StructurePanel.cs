using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Xml.Linq;

namespace StationEdit.Things.Roots
{
    public class StructurePanel : StationStructure
    {

        public StructurePanel(string prefabName, XElement thing) : base(prefabName, thing)
        {
            fill = System.Windows.Media.Brushes.White;
        }

        protected override FrameworkElement getUIElement()
        {
            System.Windows.Shapes.Rectangle myShape;
            myShape = new System.Windows.Shapes.Rectangle();
            myShape.Stroke = System.Windows.Media.Brushes.Black;
            myShape.Fill = fill;
            myShape.HorizontalAlignment = HorizontalAlignment.Left;
            myShape.VerticalAlignment = VerticalAlignment.Center;

            myShape.Tag = this;

            Orientate(myShape);

            width = (int)myShape.Width;
            height = (int)myShape.Height;

            return myShape;
        }

        protected override void Orientate(FrameworkElement myShape)
        {
            //ScaleTransform myScaleTransform;
            myShape.Height = 40;
            myShape.Width = 40;

            switch (orientation)
            {
                case Orientation.front:
                case Orientation.back:
                    myShape.Height = 8;
                    myShape.Width = 40;
                    /*myScaleTransform = new ScaleTransform();
                    myScaleTransform.ScaleX = 1;
                    myScaleTransform.ScaleY = 0.25;
                    myShape.RenderTransform = myScaleTransform;*/
                    break;

                case Orientation.left:
                case Orientation.right:
                    myShape.Height = 40;
                    myShape.Width = 8;
                    /*myScaleTransform = new ScaleTransform();
                    myScaleTransform.ScaleX = 0.25;
                    myScaleTransform.ScaleY = 1;
                    myShape.RenderTransform = myScaleTransform;*/
                    break;

                case Orientation.up:
                case Orientation.down:
                default:
                    /*myShape.Effect = new DropShadowEffect
                    {
                        Color = new Color { A = 255, R = 255, G = 255, B = 0 },
                        Direction = 135,
                        ShadowDepth = 0,
                        Opacity = .5
                    };*/
                    break;
            }
        }



    }
}
