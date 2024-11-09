using ClientApp.Entities;
using FinancialManagerApp.Models;
using Microsoft.Identity.Client.NativeInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Interaction logic for RegisterWindow1.xaml
    /// </summary>
    public partial class RegisterWindow1 : Window
    {
        FinancialManagerContext dbContext = new FinancialManagerContext();
        public RegisterWindow1()
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
            string password = PasswordTextBox.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email cannot be empty.");
                return;
            }
            var addr = new MailAddress(email);
            bool isValidData = false;

            if (string.IsNullOrEmpty(username)) MessageBox.Show("Username cannot be empty.");
            else if (username.Contains(' ')) MessageBox.Show("Username must not contain spaces.");
            else if (dbContext.Users.Any(u => u.Username == username)) MessageBox.Show("Username is already taken.");
            else if (email.Contains(' ')) MessageBox.Show("Email must not contain spaces.");
            else if (!(addr.Address == email)) MessageBox.Show("Invalid email address.");
            else if (password.Length < 8) MessageBox.Show("Password must be at least 8 characters long.");
            else if (!Regex.IsMatch(password, @"^[a-zA-Z0-9]+$")) MessageBox.Show("Password must contain only letters and numbers.");
            else isValidData = true;

            if (!isValidData) return;

            VerificationWindow._expectedCode = GenerateCode(8);
            SendCode(email, VerificationWindow._expectedCode);
            VerificationWindow verification = new VerificationWindow();
            verification.ShowDialog();

            if (VerificationWindow._expectedCode != VerificationWindow._VerificationCodeTextBox)
            {
                MessageBox.Show("_____________");
                return;
            }

            dbContext.Users.Add(new User()
            {
                Username = username,
                Email = email,
                PasswordHash = password
            });
            
            dbContext.SaveChanges();
            MessageBox.Show("You have been successfully registered. Please log in to continue.");

            Window1 login = new Window1();
            this.Close();
            login.ShowDialog();
        }






        private void SendCode(string toEmail, string code)
        {
            var fromAddress = new MailAddress("financialmanager14022@gmail.com", "Фінансовий Менеджер");
            var toAddress = new MailAddress(toEmail);
            const string fromPassword = "ensf ceio xwkq wfut";
            const string subject = "Код реєстрації";
            string body = $"Ваш код реєстрації: {code}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }
        public string GenerateCode(int length)
        {
            Random random = new Random();
            string code = "";
            for (int i = 0; i < length; i++)
            {
                code += random.Next(0, 10).ToString();  
            }
            return code;
        }
    }
}
