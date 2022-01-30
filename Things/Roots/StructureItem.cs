using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace StationEdit.Things.Roots
{
    public class StructureItem : StationStructure
    {
        public StructureItem(string prefabName, XElement thing) : base(prefabName, thing)
        {
            posz++; //inc by 1 to bump it up a bit above the other shit
        }
    }
}
