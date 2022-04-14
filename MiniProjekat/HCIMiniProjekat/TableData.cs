using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCIMiniProjekat
{
    public class TableData
    {
        public string date { get; set; }
        public double value { get; set; }

        public TableData() { }

        public TableData(string dt, double val)
        {
            date = dt;
            value = val;
        }
    }
}
