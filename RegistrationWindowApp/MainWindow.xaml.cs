using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RegistrationWindowApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void FullnameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(FullnameTextBox.Text))
                FullnameTextBlock.Visibility = Visibility.Visible;
            else
                FullnameTextBlock.Visibility = Visibility.Collapsed;
        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(EmailTextBox.Text))
                EmailTextBlock.Visibility = Visibility.Visible;
            else
                EmailTextBlock.Visibility = Visibility.Collapsed;
        }

        //private void PhoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (string.IsNullOrEmpty(PhoneTextBox.Text))
        //        PhoneTextBlock.Visibility = Visibility.Visible;
        //    else
        //        PhoneTextBlock.Visibility = Visibility.Collapsed;
        //}

        private void PasswordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(PasswordTextBox.Text))
                PasswordTextBlock.Visibility = Visibility.Visible;
            else
                PasswordTextBlock.Visibility = Visibility.Collapsed;
        }

        private void ButtonRegister(object sender, RoutedEventArgs e)
        {
            string username = FullnameTextBox.Text.Trim();
            string email = EmailTextBox.Text.Trim();
            //string phone = PhoneTextBox.Text.Trim();
            string password = PasswordTextBox.Text.Trim();
            var addr = new System.Net.Mail.MailAddress(email);
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Invalid name");
            }
            if (!(addr.Address == email))
            {
                MessageBox.Show("Invalid email");
            }
            //if (phone.Length != 10 || !Regex.IsMatch(phone, @"^\d+$"))
            //{
            //    MessageBox.Show("Invalid phone");
            //}
            if (password.Length < 8 || !Regex.IsMatch(password, @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("Invalid password");
            }
        }
    }
}