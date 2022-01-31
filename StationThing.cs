using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using System.Xml.XPath;

namespace StationEdit
{
    public class StationThing
    {
        public double posx, posy, posz;
        public double rotx, roty, rotz, rotw;
        public Orientation orientation;
        public string prefabName { get; set; }
        public string customName { get; set; }
        public XElement element;
        public int width = 10;
        public int height = 10;
        public string customColor = null;

        protected System.Windows.Media.Brush fill = System.Windows.Media.Brushes.Red;

        public StationThing(string PrefabName, XElement thing)
        {
            this.prefabName = PrefabName;
            this.element = thing;
            posx = -double.Parse(thing.XPathSelectElement("./WorldPosition/x").Value); //yup, we're flipping this
            posy = double.Parse(thing.XPathSelectElement("./WorldPosition/z").Value);
            posz = double.Parse(thing.XPathSelectElement("./WorldPosition/y").Value);
            rotx = double.Parse(thing.XPathSelectElement("./WorldRotation/eulerAngles/x").Value);
            roty = double.Parse(thing.XPathSelectElement("./WorldRotation/eulerAngles/z").Value);
            rotz = double.Parse(thing.XPathSelectElement("./WorldRotation/eulerAngles/y").Value);
            customColor = thing.XPathSelectElement("./CustomColorIndex").Value;
            customName = thing.XPathSelectElement("./CustomName").Value;
            //rotw = double.Parse(thing.XPathSelectElement("./WorldRotation/w").Value);

            calculateOrientation();
        }

        public string description 
        { get 
            {
                string text;
                text = "Position: X: " + posx + " Y: " + posy + " Z: " + posz +"\n";
                text += "Rotation: X: " + rotx + " Y: " + roty + " Z: " + rotz + "\n";
                if ((this.customColor != null) && (this.customColor != ""))
                {
                    text += "Custom Color: " + customColor + "\n";
                }
                return text;
            } 
        }


        protected virtual void calculateOrientation()
        {
            //calculate orientation
            //this is going to be messy because Stationeers doesn't use nice neat rounded angles like "90" and "180" <sigh>
            //default to "unknown"
            orientation = Orientation.unknown;

            rotx = roundRotation(rotx);
            roty = roundRotation(roty);
            rotz = roundRotation(rotz);

            if (rotx == 0 || rotx == 180 || 
                (( rotx == 90 || rotx == 270 ) && (roty == 90 || roty == 270)) )
            {
                if (rotz == 0)
                {
                    orientation = Orientation.front;
                }
                else if (rotz == 90)
                {
                    orientation = Orientation.left;
                }
                else if (rotz == 180)
                {
                    orientation = Orientation.back;
                }
                else if (rotz == 270)
                {
                    orientation = Orientation.right;
                }
            }
            else if (rotx == 90)
            {
                orientation = Orientation.down;
            }
            else if (rotx == 270)
            {
                orientation = Orientation.up;
            }
        }



        private int roundRotation(double rotation)
        {
            int workingrot = (int)Math.Round(rotation / 90) * 90;
            if (workingrot == 360)
            {
                workingrot = 90;
            }
            return workingrot;
        }

        protected void setCustomColor(string color)
        {
            //handle custom colors
            switch (color)
            {
                case "0":
                    fill = System.Windows.Media.Brushes.Blue;
                    break;
                case "1":
                    fill = System.Windows.Media.Brushes.Gray;
                    break;
                case "2":
                    fill = System.Windows.Media.Brushes.Green;
                    break;
                case "3":
                    fill = System.Windows.Media.Brushes.Orange;
                    break;
                case "4":
                    fill = System.Windows.Media.Brushes.Red;
                    break;
                case "5":
                    fill = System.Windows.Media.Brushes.Yellow;
                    break;
                case "6":
                    fill = System.Windows.Media.Brushes.White;
                    break;
                case "7":
                    fill = System.Windows.Media.Brushes.Black;
                    break;
                case "8":
                    fill = System.Windows.Media.Brushes.Brown;
                    break;
                case "9":
                    fill = System.Windows.Media.Brushes.Khaki;
                    break;
                case "10":
                    fill = System.Windows.Media.Brushes.Pink;
                    break;
                case "11":
                    fill = System.Windows.Media.Brushes.Purple;
                    break;
            }
        }

        protected virtual FrameworkElement getUIElement()
        {
            //default return a red dot
            System.Windows.Shapes.Ellipse myShape;
            myShape = new System.Windows.Shapes.Ellipse();
            myShape.Stroke = System.Windows.Media.Brushes.Black;
            myShape.Fill = fill;
            myShape.HorizontalAlignment = HorizontalAlignment.Left;
            myShape.VerticalAlignment = VerticalAlignment.Center;
            myShape.Width = width;
            myShape.Height = height;
            myShape.Tag = this;
            return myShape;
        }

        protected virtual void Orientate(FrameworkElement myShape)
        {
        }


        public virtual void DrawOnCanvas(StationCanvas canvas, Canvas subcanvas)
        {
            setCustomColor(customColor);
            FrameworkElement ele = getUIElement();
            var tt = new ThingTooltip(this);
            ele.ToolTip = tt;
            subcanvas.Children.Add(ele);
            canvas.SetShapePos(ele, posx, posy, width, height);
        }

        public int CompareTo(StationThing obj)
        {
            int workval = this.posz.CompareTo(obj.posz);
            if (workval == 0)
            {
                workval = this.orientation.CompareTo(obj.orientation);
            }
            return workval;
        }


    }
}
