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


namespace AddCategory

{

    public partial class MainWindow : Window

    {


        public string NewCategory { get; private set; }

        public string SelectedGroup { get; private set; }

        public MainWindow()

        {

            InitializeComponent();

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)

        {


            NewCategory = CategoryNameTextBox.Text;

            SelectedGroup = (GroupComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();


            if (string.IsNullOrWhiteSpace(NewCategory) || string.IsNullOrWhiteSpace(SelectedGroup))

            {

                MessageBox.Show("Please fill all fields before saving.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);

                return;

            }

            this.DialogResult = true; 

        }

    }

}