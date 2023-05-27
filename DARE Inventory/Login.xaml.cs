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

namespace DARE_Inventory
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        MainWindow mw = new MainWindow();
        public Login()
        {
            InitializeComponent();
          
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
           
            String password = "admin12345!";
            if(userBox.Text=="Ianne"|| userBox.Text ==
                "Leony"||userBox.Text ==
                "1" && passwordBox.Password ==password||passwordBox.Password=="1")
            {
                MessageBox.Show("Welcome Admin! "+userBox.Text, "Login Successful",MessageBoxButton.OK, MessageBoxImage.Information);
                mw.Show();
               // mw.adminText.Text = userBox.Text;
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrent Input! Please Try Again!","Wrong Input!",MessageBoxButton.OK, MessageBoxImage.Error);
                userBox.Clear();
                passwordBox.Clear();
            }
            

        }
    }
}
