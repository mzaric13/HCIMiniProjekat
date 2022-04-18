using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCIMiniProjekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<String> intervalsForGDP = new List<String>();
        private List<String> intervalsForTreasureYields = new List<String>();
        public LineChart lineChart { get; set; }
        public BarChart barChart { get; set; }

        public TableWindow tableWindow { get; set; }
        public ErrorWindow errorWindow { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            lineChart = new LineChart();
            barChart = new BarChart();
            tableWindow = null;
            intervalsForGDP.Add("Quarterly");
            intervalsForGDP.Add("Annual");
            intervalsForTreasureYields.Add("Daily");
            intervalsForTreasureYields.Add("Weekly");
            intervalsForTreasureYields.Add("Monthly");

            List<String> dataTypes = new List<String>();
            dataTypes.Add("GDP");
            dataTypes.Add("Treasure Yields");

            dataType.ItemsSource = dataTypes;
            dataType.SelectedIndex = 0;

            interval.ItemsSource = intervalsForGDP;
            interval.SelectedIndex = 0;
        }

        public void DrawHandler(object sender, RoutedEventArgs e) 
        {
            if (dataType.SelectedValue != null && interval.SelectedValue != null)
            {
                string dataTypeString = convertToDataTypeString(dataType.SelectedValue.ToString());
                string intervalString = interval.SelectedValue.ToString().ToLower();
                try
                {
                    fillData(dataTypeString, intervalString);
                    if (tableWindow != null)
                    {
                        tableWindow.UpdateData(barChart.tableData);
                    }
                }
                catch (Exception exception) { 
                    errorWindow = new ErrorWindow("Error: API not available.");
                    errorWindow.ShowDialog();
                }
            }
            DataContext = this;
        }

        private string convertToDataTypeString(string? v)
        {
            if (v == "GDP")
            {
                return "REAL_GDP";
            }
            else if (v == "Treasure Yields")
            {
                return "TREASURY_YIELD";
            }
            return null;
        }

        public void TableHandler(object sender, RoutedEventArgs e)
        {
            if (App.Current.Windows.Count < 2)
            {
                if (barChart.tableData.Count() == 0)
                {
                    errorWindow = new ErrorWindow("Error: There is no data to be showed.");
                    errorWindow.ShowDialog();
                }
                else
                {
                    tableWindow = new TableWindow(barChart.tableData);
                    tableWindow.Show();
                }
            }
            else
            {
                errorWindow = new ErrorWindow("Error: Table Window is already opened.");
                errorWindow.ShowDialog();
            }
        }

        public void ClearHandler(object sender, RoutedEventArgs e)
        {
            lineChart.clearData();
            barChart.clearData();
            if (tableWindow != null)
            {
                tableWindow.ClearData();
            }
        }

        public void CheckIntervals(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            var ind = comboBox.SelectedIndex;
            var selectedItem = comboBox.Items[ind];
            switch ((string)selectedItem)
            {
                case "GDP":
                    interval.ItemsSource = intervalsForGDP;
                    interval.SelectedIndex = 0;
                    break;
                case "Treasure Yields":
                    interval.ItemsSource = intervalsForTreasureYields;
                    interval.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
        }

        public void fillData(string apiSource, string interval) 
        {
            
            string query = "https://www.alphavantage.co/query?function=" + apiSource.ToUpper() + "&interval=" + interval.ToLower() + "&maturity=3month&apikey=7TRWWJRVSKBSGVYT";
            Uri queryUri = new Uri(query);

            using (WebClient client = new WebClient())
            {
                RootDataObject? jsonData = JsonSerializer.Deserialize<RootDataObject>(client.DownloadString(queryUri));
                ChartValues<double> values = new ChartValues<double>();
                if (jsonData == null)
                {
                    errorWindow = new ErrorWindow("Error: You have used API to much, there was no response.");
                    errorWindow.ShowDialog();
                    return;
                }
                barChart.tableData.Clear();
                foreach (DataObject dataObject in jsonData.data)
                {
                    if (dataObject.value == ".")
                    {
                        dataObject.value = "0.0";
                    }
                    double dataValue = Double.Parse(dataObject.value);
                    if (dataValue < barChart.minValue)
                    {
                        barChart.minValue = dataValue;
                    }
                    if (dataValue > barChart.maxValue)
                    {
                        barChart.maxValue = dataValue;
                    }
                    values.Add(dataValue);
                    barChart.datesList.Add(dataObject.date);
                    barChart.tableData.Add(new TableData(dataObject.date, dataValue));
                    lineChart.dates.Add(dataObject.date);
                    if (values.Count > 16)
                    {
                        break;
                    }
                }
                //https://stackoverflow.com/questions/64516837/livecharts-cartesian-mapping-and-configuration-with-custom-labels
                lineChart.seriesCollection.Add(new LineSeries()
                {
                    Title = interval + " " + apiSource,
                    Values = values,
                    Configuration = new CartesianMapper<double>().Y(value => value).Stroke(value => (value == values.Max()) ? Brushes.Red : (value == values.Min()) ? Brushes.Yellow : Brushes.Teal)
                                                                                        .Fill(value => (value == values.Max()) ? Brushes.Red : (value == values.Min()) ? Brushes.Yellow : Brushes.Teal),
                    PointGeometry = DefaultGeometries.Circle,
                    PointGeometrySize = 10,
                });

                barChart.seriesCollection.Add(new ColumnSeries()
                {
                    Title = interval + " " + apiSource,
                    Values = values,
                    Configuration = new CartesianMapper<double>().Y(value => value).Stroke(value => (value == values.Max()) ? Brushes.Red : (value == values.Min()) ? Brushes.Yellow: Brushes.BlueViolet)
                                                                                        .Fill(value => (value == values.Max()) ? Brushes.Red : (value == values.Min()) ? Brushes.Yellow: Brushes.LightBlue),
                });
            }
        }
    }
}
