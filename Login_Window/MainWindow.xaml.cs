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

            
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)

        {

            MessageBox.Show("Registration button clicked.");

        }
        private void RemoveText(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text == "Username or Email")
            {
                UsernameTextBox.Text = "";
                UsernameTextBox.Foreground = Brushes.Black;
            }
        }
        private void AddText(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
            {
                UsernameTextBox.Text = "Username or Email";
                UsernameTextBox.Foreground = Brushes.Gray;
            }
        }
        private void RemovePasswordText(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password == "Password")
            {
                PasswordBox.Clear();
                PasswordBox.Foreground = Brushes.Black;
            }
        }
        private void AddPasswordText(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                PasswordBox.Password = "Password";
                PasswordBox.Foreground = Brushes.Gray;
            }
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