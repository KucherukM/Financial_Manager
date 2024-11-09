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

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for VerificationWindow.xaml
    /// </summary>
    public partial class VerificationWindow : Window
    {
        public VerificationWindow()
        {
            InitializeComponent();
        }
        public static string _expectedCode { get; set; }
        public static string _VerificationCodeTextBox { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (VerificationCodeTextBox.Text == _expectedCode)
            {
                MessageBox.Show("Verification successful!");
                _VerificationCodeTextBox = VerificationCodeTextBox.Text;
                this.Close();
                return;
            }
            MessageBox.Show("Invalid code. Please try again.");
        }
    }
}
