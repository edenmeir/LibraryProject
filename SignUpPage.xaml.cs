using BookLib;
using BookLib.Logic;
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

namespace LiberaryProject
{
    public partial class SignUpPage : Window
    {
        public SignUpPage()
        {
            InitializeComponent();


        }

        private void AddCust_Btn(object sender, RoutedEventArgs e)
        {
            if (NewUserName.Text == ConfirmNewUserName.Text && NewPassword.Password == ConfirmNewPassword.Password)
            {
                Customer customer;
                if (DataIO.Instance.isCustomerExist(NewUserName.Text, out customer))
                {
                    MessageBox.Show("User Already exists");
                    return;
                }
                else if (NewUserName.Text == "" && NewPassword.Password == "")
                {
                    MessageBox.Show("Incorrect information");
                    return;
                }

                customer = new Customer(NewUserName.Text, NewPassword.Password);
                DataIO.Instance.SaveCustomer(customer);

                MessageBox.Show("New Account Signed Up");

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect Confirmation");
            }
        }

        private void ReturnToSignIn_btn(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
