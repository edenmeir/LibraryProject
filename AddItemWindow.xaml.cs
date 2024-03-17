using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using System.IO;
using BookLib;
using System.Collections.ObjectModel;
using static System.Reflection.Metadata.BlobBuilder;
using CsvHelper;
using System.Globalization;
using LiberaryProject.Data;

namespace LiberaryProject
{

    public partial class AddItemWindow : Window
    {
        public AddItemWindow()
        {
            InitializeComponent();
            cmbCategory.ItemsSource = DataController.BookCategories;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool isValidForm = false;

            if (txtName.Text == "")
            {
                txtNameValidator.Visibility = Visibility.Visible;
                isValidForm = false;
                return;
            }
            else
            {
                txtNameValidator.Visibility = Visibility.Hidden;
                isValidForm = true;
            }

            if (txtAuthor.Text == "")
            {
                txtAuthorValidator.Visibility = Visibility.Visible;
                isValidForm = false;
                return;
            }
            else
            {
                txtAuthorValidator.Visibility = Visibility.Hidden;
                isValidForm = true;
            }

            int isbn = 0;
            if (txtIsbn.Text == "")
            {
                txtIsbnValidator.Visibility = Visibility.Visible;
                isValidForm = false;
                return;
            }
            else if (!int.TryParse(txtIsbn.Text, out isbn))
            {
                txtIsbnValidator.Visibility = Visibility.Visible;
                isValidForm = false;
                return;
            }
            else
            {
                txtIsbnValidator.Visibility = Visibility.Hidden;
                isValidForm = true;
            }

            if (txtReleaseDate.SelectedDate == null)
            {
                txtDateValidator.Visibility = Visibility.Visible;
                isValidForm = false;
                return;
            }
            else
            {
                txtDateValidator.Visibility = Visibility.Hidden;
                isValidForm = true;
            }

            if (txtEdition.Text == "")
            {
                txtEditionValidator.Visibility = Visibility.Visible;
                isValidForm = false;
                return;
            }
            else
            {
                txtEditionValidator.Visibility = Visibility.Hidden;
                isValidForm = true;
            }

            if (cmbCategory.SelectedIndex == 0)
            {
                txtCategoryValidator.Visibility = Visibility.Visible;
                isValidForm = false;
                return;
            }
            else
            {
                txtCategoryValidator.Visibility = Visibility.Hidden;
                isValidForm = true;
            }

            float price = 0;
            if (txtPrice.Text == "")
            {
                txtPriceValidator.Visibility = Visibility.Visible;
                isValidForm = false;
                return;
            }
            else if (!float.TryParse(txtPrice.Text, out price))
            {
                txtPriceValidator.Visibility = Visibility.Visible;
                isValidForm = false;
                return;
            }
            else if (price == 0)
            {
                txtPriceValidator.Visibility = Visibility.Visible;
                isValidForm = false;
                return;
            }
            else
            {
                txtPriceValidator.Visibility = Visibility.Hidden;
                isValidForm = true;
            }

            if (txtDescription.Text == "")
            {
                txtDescriptionValidator.Visibility = Visibility.Visible;
                isValidForm = false;
                return;
            }
            else
            {
                txtDescriptionValidator.Visibility = Visibility.Hidden;
                isValidForm = true;
            }

            if (rdbInStock.IsChecked == false && rdbOutStock.IsChecked == false)
            {
                txtStockValidator.Visibility = Visibility.Visible;
                isValidForm = false;
                return;
            }
            else if (rdbInStock.IsChecked == false || rdbOutStock.IsChecked == true)
            {
                txtStockValidator.Visibility = Visibility.Hidden;
            }
            else if (rdbInStock.IsChecked == true || rdbOutStock.IsChecked == false)
            {
                txtStockValidator.Visibility = Visibility.Hidden;
                isValidForm = true;
            }

            if (!isValidForm)
            {
                return;
            }

            Book book = new Book
            {
                Name = txtName.Text,
                Description = txtDescription.Text,
                Author = txtAuthor.Text,
                InStock = rdbInStock.IsChecked == true ? 'V' : 'X',
                Edition = txtEdition.Text,
                ISBN = isbn,
                Price = price,
                Categories = (string)cmbCategory.SelectedValue,
                PrintDate = txtReleaseDate.SelectedDate ?? DateTime.MinValue,

            };

            DataController.BooksCollection.Add(book);
            DataIO.Instance.SaveBook(book);

        }
    }
}
