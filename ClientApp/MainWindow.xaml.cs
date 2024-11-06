using System;
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

        public MainWindow()
        {
            InitializeComponent();
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            _context = serviceProvider.GetRequiredService<FinancialManagerContext>();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(App.Configuration);
            var connectionString = App.Configuration.GetConnectionString("FinancialManagerDatabase");
            services.AddDbContext<FinancialManagerContext>(options =>
                options.UseSqlServer(connectionString));
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
            RegisterWindow1 Register = new RegisterWindow1();
            Register.ShowDialog();

        }
        private void Login(object sender, RoutedEventArgs e)
        {
            Window1 Login = new Window1();
            Login.ShowDialog();
        }
    }
}