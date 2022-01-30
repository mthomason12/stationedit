using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace StationEdit
{
    public class StationStructure : StationThing
    {

        public StationStructure(string prefabName, XElement thing) : base(prefabName, thing)
        {
        }

    }
}

