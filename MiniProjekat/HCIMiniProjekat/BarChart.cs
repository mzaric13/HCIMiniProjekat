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

        public BarChart()
        {
            seriesCollection = new SeriesCollection();
            datesList = new List<string>();
        }

        public void fillData(string apiSource, string interval)
        {
            string query = "https://www.alphavantage.co/query?function=" + apiSource.ToUpper() + "&interval=" + interval.ToLower() + "&maturity=3month&apikey=7TRWWJRVSKBSGVYT";
            Uri queryUri = new Uri(query);

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
                    datesList.Add(dataObject.date);
                    if (values.Count > 20)
                    {
                        break;
                    }
                }
                ColumnSeries columnSeries = new ColumnSeries();
                columnSeries.Title = interval + " " + apiSource;
                columnSeries.Values = values;
                columnSeries.PointGeometry = null;
                seriesCollection.Add(columnSeries);
            }
        }

        public void clearData()
        {
            seriesCollection.Clear();
            datesList.Clear();
        }
    }
}
