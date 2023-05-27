using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Text.RegularExpressions;
using System.IO;

namespace DARE_Inventory
{

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
       // MainWindow mw = new MainWindow();
        public Window1()
        {
            InitializeComponent();
            

        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
            //MessageBox.Show("Input Numbers Only", "Input Error", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void LetterValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]");
            e.Handled = regex.IsMatch(e.Text);
            MessageBox.Show("Input Letters Only", "Input Error", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        // SqlConnection con = new SqlConnection(@"Data Source=itdare\sqlexpress;Initial Catalog=DAREClinic;Integrated Security=True");
        static string path = System.IO.Path.GetFullPath(Environment.CurrentDirectory);
        static String databaseName = "DAREClinic.mdf";
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=" + path + @"\" + databaseName + ";Integrated Security=True");

        private void Button_Click(object sender, RoutedEventArgs e)
            {
                if (isValid())
                {
                SqlCommand cmd = new SqlCommand("INSERT INTO inventory VALUES (@ProductName, @Stock, @Supplier,@Price, @Treshold, @Status)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ProductName", ProductName1.Text);
                    cmd.Parameters.AddWithValue("@Stock", Stock1.Text);
                    cmd.Parameters.AddWithValue("@Supplier", Supplier1.Text);
                    cmd.Parameters.AddWithValue("@Price", Price1.Text);
                    cmd.Parameters.AddWithValue("@Treshold", Limit1.Text);
                    cmd.Parameters.AddWithValue("@Status","");
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Successfully Added", "Saved!", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();

            }
            }
        
        public bool isValid()
        {
            if (ProductName1.Text == string.Empty)
            {
                MessageBox.Show("Input Required!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Stock1.Text == string.Empty)
            {
                MessageBox.Show("Input Required!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Supplier1.Text == string.Empty)
            {
                MessageBox.Show("Input Required!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Price1.Text == string.Empty)
            {
                MessageBox.Show("Input Required!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (Limit1.Text == string.Empty)
            {
                MessageBox.Show("Input Required!", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void closeAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
