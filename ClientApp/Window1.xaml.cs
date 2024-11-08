using ClientApp.Entities;
using FinancialManagerApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        FinancialManagerContext dbContext = new FinancialManagerContext();

        public static User LoginedUser { get; set; } = null;

        public Window1()
        {
            InitializeComponent();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)

        {

            RegisterWindow1 Register = new RegisterWindow1();
            this.Close();
            Register.ShowDialog();
            

        }
        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            bool isValidData = false;

            if (string.IsNullOrEmpty(username)) MessageBox.Show("Username cannot be empty.");
            else if (username.Contains(' ')) MessageBox.Show("Username must not contain spaces.");
            else if (password.Length < 8) MessageBox.Show("Password must be at least 8 characters long.");
            else if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]+$")) MessageBox.Show("Password must contain only letters and numbers.");
            else isValidData = true;
            if (!isValidData) return;

            User user = dbContext.Users.FirstOrDefault(u => u.Username == UsernameTextBox.Text);
            if (user == null)
            {
                MessageBox.Show("No user found.");
                return;
            }
            if (user.PasswordHash == password)
            {
                LoginedUser = user;
            }
            this.Close();

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

