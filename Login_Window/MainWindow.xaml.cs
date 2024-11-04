using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System;

using System.Globalization;


namespace Login_Window

{

    public partial class MainWindow : Window

    {

        public MainWindow()

        {

            InitializeComponent();

        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)

        {

            MessageBox.Show("Registration button clicked.");

        }

    }

    public class LengthToOpacityConverter : IValueConverter

    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)

        {

            if (value is int length)

            {

                return length > 0 ? 1.0 : 0.0;

            }

            return 0.0;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)

        {

            throw new NotImplementedException();

        }

    }

}