using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClientApp;
using ClientApp.Entities;
using FinancialManagerApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client.NativeInterop;

namespace ClientApp
{
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; }

        static App()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
    public partial class MainWindow : Window
    {
        private readonly FinancialManagerContext _context;
        public static ObservableCollection<Transaction> Transactions { get; set; }
        FinancialManagerContext dbContext = new FinancialManagerContext();

        public MainWindow()
        {
            InitializeComponent();
            this.Title = Window1.LoginedUser.Username;
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<FinancialManagerContext>();
            Transactions = new ObservableCollection<Transaction>();
            DataContext = this;
            LoadTransactions();

        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(App.Configuration);
            var connectionString = App.Configuration.GetConnectionString("FinancialManagerDatabase");
            services.AddDbContext<FinancialManagerContext>(options =>
                options.UseSqlServer(connectionString));
        }
        private async void LoadTransactions()
        {
            try
            {
                if (Window1.LoginedUser == null)
                {
                    MessageBox.Show("Користувач не залогінений.");
                    return;
                }

                var transactionsFromDb = await _context.Transactions
                    .Where(t => t.UserId == Window1.LoginedUser.UserId)
                    .Include(t => t.Category)
                    .ToListAsync();

                foreach (var transaction in transactionsFromDb)
                {
                    Transactions.Add(transaction);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні транзакцій: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var userCount = _context.Users.Count();
                MessageBox.Show($"Успішне з'єднання! Кількість користувачів: {userCount}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка з'єднання з базою даних: {ex.Message}");
            }
        }
        private void Register(object sender, RoutedEventArgs e)
        {
            
            if(MessageBox.Show("Title","Are you sure",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Window1 Login = new Window1();
                this.Close();
                Login.ShowDialog();

                _context.Users.Remove(Window1.LoginedUser);
                _context.SaveChanges();

                Window1.LoginedUser = null;
                Transactions = new ObservableCollection<Transaction>();
            }
                
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            Window1 Login = new Window1();
            this.Close();
            Login.ShowDialog();
            Window1.LoginedUser = null;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<Category> categories = dbContext.Categories.ToList();
            if (categories == null || categories.Count == 0)
            {
                MessageBox.Show("No categories found. Please add categories.");
                return;
            }
            AddTransactionWindow transactionWindow = new AddTransactionWindow();
            
            transactionWindow.ShowDialog();
        }

        private void DeleteTransaction_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Transaction transaction)
            {
                if (MessageBox.Show("Ви впевнені, що хочете видалити цю транзакцію?", "Підтвердження", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        _context.Transactions.Remove(transaction);
                        _context.SaveChanges();

                        Transactions.Remove(transaction);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка при видаленні транзакції: {ex.Message}");
                    }
                }
            }
        }
    }
}