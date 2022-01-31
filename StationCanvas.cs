using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Diagnostics;
using System.Windows.Media.Effects;

namespace StationEdit
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:StationEdit"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:StationEdit;assembly=StationEdit"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:StationCanvas/>
    ///
    /// </summary>
    public class StationCanvas : Canvas
    {
        XDocument file;
        MainWindow mw;
        Dictionary<int, Canvas> subCanvas = new Dictionary<int, Canvas>();

        System.Windows.Shapes.Rectangle baseRect;

        IEnumerable<XElement> xmlThings;
        public List<StationThing> things = new List<StationThing>();
        public List<StationStructure> structures = new List<StationStructure>();
        public List<String> unhandled = new List<String>();
        public double minx, miny, minz, maxx, maxy, maxz;

        static StationCanvas()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(StationCanvas), new FrameworkPropertyMetadata(typeof(StationCanvas)));
        }

        public void setMainWindow(MainWindow mw)
        {
            this.mw = mw;
        }

        public void setFile(XDocument aFile)
        {
            file = aFile;
            reload();
        }

        void reload()
        {
            Children.Clear();
            subCanvas.Clear();
            structures.Clear();
            things.Clear();
            //get all Things
            xmlThings = file.XPathSelectElements("/WorldData/Things/ThingSaveData");

            //get structures and map bounds

            foreach (XElement thing in xmlThings)
            {
                /*foreach (XAttribute attr in thing.Attributes())
                {
                    Debug.WriteLine(attr.Name.ToString());
                }*/
                XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
                //XName ThingType = xsi + "type";
                if (thing.Attribute(xsi + "type") != null)
                {
                    if (new[]{ "StructureSaveData","PipeSaveData","DoorSaveData", "SolarPanelSaveData", "MachineSaveData", "DeviceAtmosphericSaveData" }
                        .Contains(thing.Attribute(xsi + "type").Value))
                    {
                        string prefabName = thing.XPathSelectElement("./PrefabName").Value;
                        StationStructure newStructure = StationStructureFactory.MakeStructure(prefabName, thing);
                        if (newStructure.GetType() == typeof(StationStructure))
                        {
                            unhandled.Add(prefabName);
                        }
                        things.Add(newStructure);
                        structures.Add(newStructure);
                    }
                    else
                    {
                        //list the whole type as unhandled
                        unhandled.Add("{" + thing.Attribute(xsi + "type").Value + "}");
                    }

                }
            }

            Debug.WriteLine("Unhandled Things:");
            //debug list of things that weren't handled
            unhandled.Sort();
            foreach (String unhandledThing in unhandled.Distinct())
            {
                Debug.WriteLine("  "+unhandledThing);
            }

            minx = structures.Min(x => x.posx);
            maxx = structures.Max(x => x.posx);
            miny = structures.Min(x => x.posy);
            maxy = structures.Max(x => x.posy);
            minz = structures.Min(x => x.posz);
            maxz = structures.Max(x => x.posz);

            this.Width = TranslateX(maxx+2);
            this.Height = TranslateY(maxy+2);

            //draw an empty rect at the bottom Z-index to capture mouse events
            baseRect = new System.Windows.Shapes.Rectangle();
            baseRect.Stroke = System.Windows.Media.Brushes.Black;
            baseRect.Fill = System.Windows.Media.Brushes.White;
            baseRect.HorizontalAlignment = HorizontalAlignment.Left;
            baseRect.VerticalAlignment = VerticalAlignment.Center;
            baseRect.Height = this.Height;
            baseRect.Width = this.Width;
            Children.Add(baseRect);
            SetLeft(baseRect, 0);
            SetTop(baseRect, 0);

            //sort things and structures by Z position
            things.Sort((a, b) => a.CompareTo(b));
            structures.Sort((a, b) => a.CompareTo(b));

            //create a canvas for each level
            for (int canv = (int)Math.Truncate(minz); canv < (int)Math.Truncate(maxz + 1) ; canv++)
            {
                Canvas newCanvas = new Canvas();
                //drop shadow for depth
                newCanvas.Effect = new DropShadowEffect
                {
                    Color = new Color { A = 255, R = 0, G = 0, B = 0 },
                    Direction = 315,
                    ShadowDepth = 8,
                    Opacity = 0.5
                };
                //cache for speed
                newCanvas.CacheMode = new BitmapCache {
                    EnableClearType = false,
                    RenderAtScale = 2,
                    SnapsToDevicePixels = false
                };
                Children.Add(newCanvas);
                SetLeft(newCanvas, 0);
                SetTop(newCanvas, 0);
                subCanvas.Add(canv, newCanvas);
            }

            foreach (StationStructure thing in structures)
            {
                thing.DrawOnCanvas(this, subCanvas[(int)Math.Truncate(thing.posz)]);
            }

        }

        public int TranslateX(double pos)
        {
            int workingpos;
            //we need to flip the X-axis
            workingpos = (int)(pos * 20);
            workingpos = workingpos - (int)(minx * 20);
            return workingpos;
        }

        public int TranslateY(double pos)
        {
            int workingpos;
            workingpos = (int)(pos * 20);
            workingpos = workingpos - (int)(miny * 20);
            return workingpos;
        }

        public void SetShapePos(FrameworkElement shape, double posx, double posy, double width, double height)
        {
            SetLeft(shape, TranslateX(posx)+((40-width)/2));
            SetTop(shape, TranslateY(posy)+((40-height)/2));
        }

        public void SetMaxLevel(int maxlevel)
        {
            // show/hide levels as appropriate
            foreach (KeyValuePair<int,Canvas> subc in subCanvas)
            {
                if (subc.Key > maxlevel)
                {
                    ((Canvas)subc.Value).Visibility = Visibility.Hidden;
                }
                else
                {
                    ((Canvas)subc.Value).Visibility = Visibility.Visible;
                }
            }
        }

    }
}
