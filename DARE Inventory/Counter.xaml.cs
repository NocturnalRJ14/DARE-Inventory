using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DARE_Inventory
{
    /// <summary>
    /// Interaction logic for Counter.xaml
    /// </summary>
    public partial class Counter : Window
    {
        MainWindow mw = new MainWindow();
        public Counter()
        {
            InitializeComponent();

        }



        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            int numY = Int32.Parse(textCounter.Text);
            int superpapa = numY + 1;
            textCounter.Text = superpapa.ToString();




        }

        private void minusButtton_Click(object sender, RoutedEventArgs e)
        {
            int numY = Int32.Parse(textCounter.Text);
            int superpapa = numY - 1;
            textCounter.Text = superpapa.ToString();
        }
        //   SqlConnection con = new SqlConnection(@"Data Source=itdare\sqlexpress;Initial Catalog=DAREClinic;Integrated Security=True");
        static string path = System.IO.Path.GetFullPath(Environment.CurrentDirectory);
        static String databaseName = "DAREClinic.mdf";
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=" + path + @"\" + databaseName + ";Integrated Security=True");

        private void reUp_Click(object sender, RoutedEventArgs e)
        {

            String prod1 = sampleText.Text;
            int stock1 = Int32.Parse(textCounter.Text);
            SqlCommand cmd = new SqlCommand("update inventory set Stock = @stock1 where ProductName = @prod1",con);
           // SqlCommand cmd = new SqlCommand("UPDATE MyDataTable ('Stock') VALUES (@stock1) WHERE ProductName = @prod1", SqlConn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@prod1", sampleText.Text);
            cmd.Parameters.AddWithValue("@stock1", textCounter.Text);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Successfully Updated!", "Updated!", MessageBoxButton.OK, MessageBoxImage.Information);



        }

        private void textCounter_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
