using StationEdit.Things.Roots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StationEdit.Things.Structures
{
    public class StructureFrameIron : StructureFrame
    {
        public StructureFrameIron(string prefabName, XElement thing) : base(prefabName, thing)
        {
        }
    }
}
