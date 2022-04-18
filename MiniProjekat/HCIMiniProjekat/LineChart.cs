using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HCIMiniProjekat
{
    public class LineChart
    {
        //https://stackoverflow.com/questions/63515972/how-can-i-update-the-chart-in-livechart-based-on-array-value-with-a-button
        public SeriesCollection seriesCollection { get; set; }
        public List<string> dates { get; set; }
        public LineChart()
        {
            seriesCollection = new SeriesCollection();
            dates = new List<string>();
        }

        public void clearData()
        {
            seriesCollection.Clear();
            dates.Clear();
        }
    }
}
