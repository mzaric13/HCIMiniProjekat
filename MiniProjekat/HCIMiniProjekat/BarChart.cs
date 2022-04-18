using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Wpf;
using System.Net;
using System.Text.Json;

namespace HCIMiniProjekat
{
    public class BarChart
    {
        public SeriesCollection seriesCollection { get; set; }
        public List<string> datesList { get; set; }

        public List<TableData> tableData { get; set; }
        public double minValue { get; set; }
        public double maxValue { get; set; }

        public BarChart()
        {
            seriesCollection = new SeriesCollection();
            datesList = new List<string>();
            tableData = new List<TableData>();
            minValue = 10000000;
            maxValue = -10000000;
        }

        public void clearData()
        {
            seriesCollection.Clear();
            datesList.Clear();
            tableData.Clear();
            minValue = 10000000;
            maxValue = -10000000;
        }
    }
}
