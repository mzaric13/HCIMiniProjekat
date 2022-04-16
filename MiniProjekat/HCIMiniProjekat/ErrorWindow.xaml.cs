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
using System.Windows.Shapes;

namespace HCIMiniProjekat
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow(string message)
        {
            InitializeComponent();
            Uri uri = new Uri("../../../error.png", UriKind.RelativeOrAbsolute);
            errorPicture.Source = BitmapFrame.Create(uri);
            errorMessage.Content = message;
        }

        public void CloseHandler(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
