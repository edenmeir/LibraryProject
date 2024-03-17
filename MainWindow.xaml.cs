using LiberaryProject.Data;
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
using BookLib;
using BookLib.Logic;

namespace LiberaryProject
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            StorePage sp = new StorePage();
            if (AdminUserName.Text == "Admin" && AdminPassword.Password == "Admin")
            {
                sp.Show();
                this.Close();
                return;
            }

            Customer customer;
            if (DataIO.Instance.isCustomerExist(CustomerUserName.Text, out customer))
            {
                if (customer.Password == CustomerPassword.Password)
                {
                    sp.AddItemBtn.Visibility = Visibility.Hidden;
                    sp.AdminHeader.Visibility = Visibility.Hidden;
                    sp.Show();
                    this.Close();
                    return;
                }
            }

            MessageBox.Show("Incorrect Username Or Password");
        }

        private void SignUp_Btn(object sender, RoutedEventArgs e)
        {
            SignUpPage su = new SignUpPage();
            su.Show();
            this.Close();

        }
    }
}
