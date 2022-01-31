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
using Microsoft.Win32;

namespace StationEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public XDocument file;
        ScaleTransform st = new ScaleTransform();
        Point? lastCenterPositionOnTarget;
        Point? lastMousePositionOnTarget;
        Point? lastDragPoint;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        void OpenCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Stationeers World Files|world.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                DoDebug("Loading " + openFileDialog.FileName);
                file = XDocument.Load(openFileDialog.FileName);
                DoDebug("Loaded");
                xmlItems.DataContext = file;
                canvas.setMainWindow(this);
                canvas.setFile(file);
                DoDebug("Structures: " + canvas.structures.Count());
                DoDebug("Min X:" + canvas.minx);
                DoDebug("Min Y:" + canvas.miny);
                DoDebug("Min Z:" + canvas.minz);
                DoDebug("Max X:" + canvas.maxx);
                DoDebug("Max Y:" + canvas.maxy);
                DoDebug("Max Z:" + canvas.maxz);
                DoDebug("Canvas Width:" + canvas.Width);
                DoDebug("Canvas Height:" + canvas.Height);
                maxLevel.Value = (int)canvas.maxz;
                maxLevel.Minimum = (int)canvas.minz;
                maxLevel.Maximum = (int)canvas.maxz;
            }
        }

        public void DoDebug(string str)
        {
            DebugBox.AppendText(str+"\r\n");
            DebugBox.ScrollToEnd();
        }

        private void xmlItems_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            XElement node = xmlItems.SelectedItem as XElement;
            /*if (node is XText)
            {
                DoDebug(node.Name.ToString());
                DoDebug(node.Value);
            }*/
        }

        private void canvas_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        private void maxLevel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            canvas.SetMaxLevel((int)maxLevel.Value);
        }

        private void ScrollViewer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        private void scrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (zoomBox.SelectedIndex > 0)
                {
                    zoomBox.SelectedIndex -= 1;
                }
            }
            else
            {
                if (zoomBox.SelectedIndex < zoomBox.Items.Count - 1)
                {
                    zoomBox.SelectedIndex += 1;
                }
            }
            e.Handled = true;
        }

        private void scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                Point? targetBefore = null;
                Point? targetNow = null;

                if (!lastMousePositionOnTarget.HasValue)
                {
                    if (lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new Point(scrollViewer.ViewportWidth / 2,
                                                         scrollViewer.ViewportHeight / 2);
                        Point centerOfTargetNow =
                              scrollViewer.TranslatePoint(centerOfViewport, canvas);

                        targetBefore = lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(canvas);

                    lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    double dXInTargetPixels = targetNow.Value.X - targetBefore.Value.X;
                    double dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    double multiplicatorX = e.ExtentWidth / canvas.Width;
                    double multiplicatorY = e.ExtentHeight / canvas.Height;

                    double newOffsetX = scrollViewer.HorizontalOffset -
                                        dXInTargetPixels * multiplicatorX;
                    double newOffsetY = scrollViewer.VerticalOffset -
                                        dYInTargetPixels * multiplicatorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }

                    scrollViewer.ScrollToHorizontalOffset(newOffsetX);
                    scrollViewer.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }

        private void scrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (lastDragPoint.HasValue)
            {
                Point posNow = e.GetPosition(scrollViewer);

                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;

                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (canvas != null) //will be null during init so don't do anything
            {
                lastMousePositionOnTarget = Mouse.GetPosition(canvas);

                canvas.LayoutTransform = st;

                //get zoom from zoomBox
                string zoomString = (string)((ComboBoxItem)zoomBox.SelectedItem).Content;
                //remove the percentage sign
                zoomString = zoomString.Remove(zoomString.Length - 1, 1);
                double zoomVal = double.Parse(zoomString) / 100;
                st.ScaleX = zoomVal;
                st.ScaleY = zoomVal;

                var centerOfViewport = new Point(scrollViewer.ViewportWidth / 2,
                                                         scrollViewer.ViewportHeight / 2);
                lastCenterPositionOnTarget = scrollViewer.TranslatePoint(centerOfViewport, canvas);
            }
        }
    }
}
