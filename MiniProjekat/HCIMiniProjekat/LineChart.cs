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
        //public Func<double, string> YAxis { get; set; }
        public LineChart()
        {
            seriesCollection = new SeriesCollection();
            dates = new List<string>();
            //YAxis = value => value.
        }
        public void fillData(string apiSource, string interval)
        {
            //apiSource treba da bude REAL_GDP ili TREASURY_YIELD
            string QUERY_URL = "https://www.alphavantage.co/query?function=" + apiSource.ToUpper() + "&interval=" + interval.ToLower() + "&maturity=3month&apikey=7TRWWJRVSKBSGVYT";
            Uri queryUri = new Uri(QUERY_URL);

            using (WebClient client = new WebClient())
            {
                RootDataObject? jsonData = JsonSerializer.Deserialize<RootDataObject>(client.DownloadString(queryUri));
                ChartValues<double> values = new ChartValues<double>();
                foreach (DataObject dataObject in jsonData.data)
                {
                    if (dataObject.value == ".")
                    {
                        dataObject.value = "0.0";
                    }
                    double dataValue = Double.Parse(dataObject.value);
                    values.Add(dataValue);
                    dates.Add(dataObject.date);
                    if (values.Count > 20)
                    {
                        break;
                    }
                }
                LineSeries lineSeries = new LineSeries();
                lineSeries.Title = interval + " " + apiSource;
                lineSeries.Values = values;
                lineSeries.PointGeometry = null;
                seriesCollection.Add(lineSeries);
            }
        }

        public void clearData()
        {
            seriesCollection.Clear();
            dates.Clear();
        }
    }
}
