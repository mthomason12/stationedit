using StationEdit.Things.Roots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace StationEdit.Things.Structures
{
    public class StructureCompositeDoor : StructureDoor
    {
        public StructureCompositeDoor(string prefabName, XElement thing) : base(prefabName, thing)
        {
            fill = System.Windows.Media.Brushes.White;
        }


    }
}
