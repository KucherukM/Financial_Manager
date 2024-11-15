using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Categories_Window

{

    public partial class MainWindow : Window

    {

        public MainWindow()

        {

            InitializeComponent();

        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)

        {

            var addCategoryWindow = new AddCategory.MainWindow();

            bool? result = addCategoryWindow.ShowDialog();

            if (result == true)

            {


                string newCategory = addCategoryWindow.NewCategory;
                
                string group = addCategoryWindow.SelectedGroup;


                AddCategoryToUI(newCategory, group);

            }

        }

        private void AddCategoryToUI(string category, string group)

        {


            var newCategoryBlock = new TextBlock

            {

                Text = category,

                Width = 120,

                Margin = new Thickness(5),

                Background = System.Windows.Media.Brushes.LightGray,

                Padding = new Thickness(10),

                TextAlignment = TextAlignment.Center

            };


            WrapPanel targetWrapPanel = FindWrapPanelByGroup(group);

            if (targetWrapPanel != null)

            {

                targetWrapPanel.Children.Add(newCategoryBlock);

            }

            else

            {

                MessageBox.Show($"Group '{group}' not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private WrapPanel FindWrapPanelByGroup(string group)

        {

            if (group == "Group 1") return DailyWrapPanel;

            if (group == "Group 2") return ActivitiesWrapPanel; 

            return null;

        }

    }

}