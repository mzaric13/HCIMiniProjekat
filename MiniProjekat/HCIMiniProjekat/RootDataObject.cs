using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIMiniProjekat
{
    internal class RootDataObject
    {
        public string name { get; set; }
        public string interval { get; set; }
        public string unit { get; set; }
        public List<DataObject> data { get; set; }

        public RootDataObject()
        {

        }

    }
}
