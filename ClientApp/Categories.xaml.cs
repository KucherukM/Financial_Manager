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
    /// Interaction logic for Categories.xaml
    /// </summary>
    public partial class Categories : Window
    {
        FinancialManagerContext dbContext = new FinancialManagerContext();
        public Categories()
        {
            InitializeComponent();
            Category catgory = dbContext.Categories.FirstOrDefault();
            TextBlock1.Text= catgory?.Name;
        }

    }
}
