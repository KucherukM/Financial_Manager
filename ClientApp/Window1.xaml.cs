﻿using ClientApp.Entities;
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

            string email_username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();
            bool isValidData = false;

            User nameUser = dbContext.Users.FirstOrDefault(u => u.Username == email_username);
            User emailUser = dbContext.Users.FirstOrDefault(u => u.Email == email_username);

            if (emailUser == null && nameUser == null)
            {
                MessageBox.Show("No user found.");
                return;
            }
            if (emailUser?.PasswordHash == password) LoginedUser = emailUser;
            else if (nameUser?.PasswordHash == password) LoginedUser = nameUser;
            else { MessageBox.Show("incorect password"); return; } 
            MessageBox.Show($"Succesfull login");
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
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

