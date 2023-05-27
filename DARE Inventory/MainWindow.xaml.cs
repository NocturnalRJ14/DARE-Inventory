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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace DARE_Inventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public MainWindow()
        {
            
            InitializeComponent();
            LoadInventory();
        }

        //SqlConnection con = new SqlConnection(@"Data Source=itdare\sqlexpress;Initial Catalog=DAREClinic;Integrated Security=True");
        static string path = System.IO.Path.GetFullPath(Environment.CurrentDirectory);
        static String databaseName = "DAREClinic.mdf";
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;AttachDbFilename=" + path+@"\"+databaseName+";Integrated Security=True");

        public void LoadInventory()
        {
            timeBlock.Text = DateTime.Now.ToString("h:mm:ss tt");
            SqlCommand cmd = new SqlCommand("select * from  inventory", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            dt.Load(dr);
            inventorygrid.ItemsSource = dt.DefaultView;
            String status = "";
            int i,xy=0;
            for (i = 0; i <= dt.Rows.Count; i++)//ROW COUNT IS 6
            {
                try
                {
                    int stockC = Convert.ToInt32(dt.Rows[i]["Stock"]);
                    int tresholdC = Convert.ToInt32(dt.Rows[i]["Treshold"]);
                    xy = i + 1;
                    if (stockC > tresholdC)
                    {
                        status = "On-Stock";
                        cmd = new SqlCommand("update inventory set Status = @status where ID = @xy", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@xy", xy);
                        cmd.ExecuteNonQuery();
                    }
                    else if (stockC < tresholdC)
                    {
                        status = "No Stock";
                        cmd = new SqlCommand("update inventory set Status = @status where ID = @xy", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@xy", xy);
                        cmd.ExecuteNonQuery();
                    }
                    // BUT THE COUNT IN INDEX IS 5
                }
                catch (Exception ex)
                {
                }
                //adminText.Text = dt.Rows[0]["ProductName"].ToString();
                }
           
            con.Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window1 f1 = new Window1();
            f1.Show();
        }
        private void Supplier_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Ellipse_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoadInventory();

        }
        private void Ellipse_MouseEnter(object sender, MouseEventArgs e)
        {
            updateButton.Width = 40;
            updateButton.Height = 40;  
        }

        private void updateButton_MouseLeave(object sender, MouseEventArgs e)
        {
            updateButton.Width = 47;
            updateButton.Height = 47;
        }

        private void updateButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            updateButton.Width = 60;
            updateButton.Height = 60;
        }
        private void prodName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void inventorygrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataGrid gd =(DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                prodName.Text = row_selected["ProductName"].ToString();
            }
        }

        private void inventorygrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Counter cn = new Counter();
            cn.Show();

            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                //int num = Convert.ToInt32(row_selected["Stock"]);
                cn.textCounter.Text = row_selected["Stock"].ToString();
                String prodName = row_selected["ProductName"].ToString();
                cn.sampleText.Text = row_selected["ProductName"].ToString();

            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
           
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("You are now Exiting the App.", "Exit", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            this.Close();
        }
    }
}