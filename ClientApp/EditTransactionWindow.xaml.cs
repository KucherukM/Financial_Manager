using ClientApp.Entities;
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
    /// Interaction logic for EditTransactionWindow.xaml
    /// </summary>
    public partial class EditTransactionWindow : Window
    {
        private Transaction _transaction;

        public EditTransactionWindow(Transaction transaction)
        {
            InitializeComponent();
            _transaction = transaction;
            DataContext = _transaction;
            MessageBox.Show(transaction.Category.Name);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Save changes (assumes binding to properties directly)
            DialogResult = true;
            Close();
        }
    }
}
