using ClientApp.Entities;
using FinancialManagerApp.Models;
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
    /// Interaction logic for AddTransactionWindow.xaml
    /// </summary>
    public partial class AddTransactionWindow : Window
    {
        FinancialManagerContext dbContext = new FinancialManagerContext();
        public AddTransactionWindow()
        {
            InitializeComponent();
            LoadCategories();
            LoadDays();
            LoadMonths();
            LoadYears();
        }
        private void LoadCategories()
        {
            List<Category> categories = dbContext.Categories.ToList();
            categoryComboBox.ItemsSource = categories;
            categoryComboBox.DisplayMemberPath = "Name";
            categoryComboBox.SelectedValuePath = "Id";
        }
        private void LoadDays()
        {
            for (int i = 1; i <= 31; i++)
                dayComboBox.Items.Add(i);
        }
        private void LoadMonths()
        {
            for (int i = 1; i <= 12; i++)
                monthComboBox.Items.Add(i);
        }
        private void LoadYears()
        {
            for (int i = DateTime.Now.Year - 20; i <= DateTime.Now.Year; i++)
                yearComboBox.Items.Add(i);
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;

            if (datePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = datePicker.SelectedDate.Value;
                dayComboBox.SelectedItem = selectedDate.Day;
                monthComboBox.SelectedItem = selectedDate.Month;
                yearComboBox.SelectedItem = selectedDate.Year;
            }
        }

        private void NoteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(NoteTextBox.Text))
                NoteTextBlock.Visibility = Visibility.Visible;
            else
                NoteTextBlock.Visibility = Visibility.Collapsed;
        }

        private void AmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(AmountTextBox.Text))
                AmountTextBlock.Visibility = Visibility.Visible;
            else
                AmountTextBlock.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int selectedCategoryId = (categoryComboBox.SelectedItem as Category).CategoryId;
            decimal amount = Convert.ToDecimal(AmountTextBox.Text);
            int day = (int)dayComboBox.SelectedItem;
            int month = (int)monthComboBox.SelectedItem;
            int year = (int)yearComboBox.SelectedItem;
            string note = NoteTextBox.Text;
            bool isValidData = false;

            if (note.Length > 30) MessageBox.Show("Note is too long");
            else if (string.IsNullOrEmpty(note)) MessageBox.Show("Note is empty");
            else isValidData = true;
            if (!isValidData) return;

            Account account = dbContext.Accounts.FirstOrDefault(a => a.UserId == Window1.LoginedUser.UserId);
            if (account == null)
            {
                dbContext.Accounts.Add(new Account()
                {
                    AccountName = Window1.LoginedUser.Username,
                    Balance = 0,
                    UserId = Window1.LoginedUser.UserId
                });
                dbContext.SaveChanges();
            }

            DateTime date = new DateTime(year, month, day);
            Transaction transaction = new Transaction()
            {
                Amount = amount,
                Date = date,
                Note = note,
                UserId = Window1.LoginedUser.UserId,
                CategoryId = selectedCategoryId,
                AccountId = dbContext.Accounts.FirstOrDefault(a => a.UserId == Window1.LoginedUser.UserId).AccountId
            };

            dbContext.Transactions.Add(transaction);
            dbContext.SaveChanges();
            MainWindow.Transactions.Add(transaction);
            MessageBox.Show("Transaction successfully added.");

            
            this.Close();
            
        }
    }
}
