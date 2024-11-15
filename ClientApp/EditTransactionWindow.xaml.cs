using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ClientApp.Entities;
using FinancialManagerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientApp
{
    public partial class EditTransactionWindow : Window
    {
        private Transaction _transaction;
        private FinancialManagerContext _context;
        public class Date
        {
            public Date(int value) { Name = value.ToString(); DateValue = value; }
            public string Name { get; set; }
            public int DateValue { get; set; }
        }
        public List<Date> days_list = new List<Date>();
        public List<Date> months_list = new List<Date>();
        public List<Date> years_list = new List<Date>();
        private Date _days;
        private Date _months;
        private Date _years;
        public int Days
        {
            get { return _days.DateValue; }
            set { _days = new Date(value); }
        }
        public int Months
        {
            get { return _months.DateValue; }
            set { _months = new Date(value); }
        }
        public int Years
        {
            get { return _years.DateValue; }
            set { _years = new Date(value); }
        }

        public EditTransactionWindow(Transaction transaction, FinancialManagerContext context)
        {
            InitializeComponent();
            _transaction = transaction;
            _context = context;
            LoadCategories();
            LoadDays();
            LoadMonths();
            LoadYears();
            LoadTransactionData();
        }

        private void LoadCategories()
        {
            CategoryBox.Text = _transaction.Category.CategoryId.ToString();
            CategoryComboBox.ItemsSource = _context.Categories.ToList();
            CategoryComboBox.DisplayMemberPath = "Name";
            CategoryComboBox.SelectedValuePath = "Id";
        }
        private void LoadDays()
        {
            //dayComboBox.Items.Clear();
            for (int i = 1; i <= 31; i++)
            {
                //dayComboBox.Items.Add(i);
                days_list.Add(new Date(i));
            }
            dayComboBox.ItemsSource = days_list;
        }
        private void LoadMonths()
        {
            //monthComboBox.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                //monthComboBox.Items.Add(i);
                months_list.Add(new Date(i));
            }
            monthComboBox.ItemsSource = months_list;
        }
        private void LoadYears()
        {
            //yearComboBox.Items.Clear();
            for (int i = DateTime.Now.Year - 20; i <= DateTime.Now.Year; i++)
            {
                //yearComboBox.Items.Add(i);
                years_list.Add(new Date(i));
            }
            yearComboBox.ItemsSource = years_list;
        }

        private void datePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;

            if (datePicker.SelectedDate.HasValue)
            {
                DateTime selectedDate = datePicker.SelectedDate.Value;
                Days = selectedDate.Day;
                Months = selectedDate.Month;
                Years = selectedDate.Year;
            }
        }

        private void LoadTransactionData()
        {
            Days = _transaction.Date.Day;
            Months = _transaction.Date.Month;
            Years = _transaction.Date.Year;
            AmountTextBox.Text = _transaction.Amount.ToString("F2");
            DatePicker.SelectedDate = _transaction.Date;
            NoteTextBox.Text = _transaction.Note;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CategoryBox.Text))
            {
                MessageBox.Show("Please select a category.");
                return;
            }

            if (string.IsNullOrEmpty(AmountTextBox.Text) || !decimal.TryParse(AmountTextBox.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount.");
                return;
            }

            if (string.IsNullOrEmpty(NoteTextBox.Text))
            {
                MessageBox.Show("Note cannot be empty.");
                return;
            }
            else if (NoteTextBox.Text.Length > 30)
            {
                MessageBox.Show("Note is too long. Maximum length is 30 characters.");
                return;
            }

            _context.Transactions.Remove(_transaction);

            MainWindow.Transactions.Remove(_transaction);
            _transaction.Amount = amount;
            _transaction.Date = DatePicker.SelectedDate ?? DateTime.Now;
            _transaction.Note = NoteTextBox.Text;
            _transaction.CategoryId = (int)(CategoryComboBox.SelectedValue == null ? CategoryComboBox.SelectedValue : CategoryBox.Text);
            MainWindow.Transactions.Add(_transaction);

            _context.Transactions.Add(_transaction);

            _context.SaveChanges();
            this.DialogResult = true;
            this.Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NoteTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            NoteTextBlock.Visibility = string.IsNullOrEmpty(NoteTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void AmountTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            AmountTextBlock.Visibility = string.IsNullOrEmpty(AmountTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}