using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public MainWindow()
        {
            InitializeComponent();
            intervalsForGDP.Add("Quarterly");
            intervalsForGDP.Add("Annual");
            intervalsForTreasureYields.Add("Daily");
            intervalsForTreasureYields.Add("Weekly");
            intervalsForTreasureYields.Add("Monthly");

            List<String> dataTypes = new List<String>();
            dataTypes.Add("GDP");
            dataTypes.Add("Tresure Yields");

            dataType.ItemsSource = dataTypes;
            dataType.SelectedIndex = 0;

            interval.ItemsSource = intervalsForGDP;
            interval.SelectedIndex = 0;
        }

        public void DrawHandler(object sender, RoutedEventArgs e) { }

        public void TableHandler(object sender, RoutedEventArgs e) { }

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
                case "Tresure Yields":
                    interval.ItemsSource = intervalsForTreasureYields;
                    interval.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
        }
    }
}
