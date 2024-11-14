using System;
using System.Collections.Generic;
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
            CategoryComboBox.ItemsSource = _context.Categories.ToList();
            CategoryComboBox.DisplayMemberPath = "Name";
            CategoryComboBox.SelectedValuePath = "Id";
        }
        private void LoadDays()
        {
            dayComboBox.Items.Clear();
            for (int i = 1; i <= 31; i++)
                dayComboBox.Items.Add(i);
        }
        private void LoadMonths()
        {
            monthComboBox.Items.Clear();
            for (int i = 1; i <= 12; i++)
                monthComboBox.Items.Add(i);
        }
        private void LoadYears()
        {
            yearComboBox.Items.Clear();
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

        private void LoadTransactionData()
        {
            AmountTextBox.Text = _transaction.Amount.ToString("F2");
            DatePicker.SelectedDate = _transaction.Date;
            NoteTextBox.Text = _transaction.Note;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem == null)
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
            _transaction.CategoryId = (int)(CategoryComboBox.SelectedValue ?? 0);
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