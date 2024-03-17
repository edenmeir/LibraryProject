using BookLib;
using LiberaryProject.Data;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LiberaryProject
{
    public partial class StorePage : Window
    {

        public StorePage()
        {
            InitializeComponent();

            DataController.BooksCollection = Data.DataIO.Instance.GetAllBooks();
            BooksView.ItemsSource = DataController.BooksCollection;
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            AddItemWindow addItemWindow = new AddItemWindow();
            addItemWindow.Show();
        }
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Logged out!");
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();
        }

        private void OnCategoryFilter(object sender, RoutedEventArgs e)
        {
            string category = (string)((Button)sender).Content;

            var filteredBooks = (from b in DataController.BooksCollection
                                 where b.Categories == category
                                 select b).ToList<Book>();

            BooksView.ItemsSource = filteredBooks;

            BooksView.Items.Refresh();
        }

        private void RefreshBtn(object sender, RoutedEventArgs e)
        {
            var filteredBooks = DataController.BooksCollection.ToList<Book>();
            BooksView.ItemsSource = filteredBooks;
            BooksView.Items.Refresh();
        }

        private void MagnifireSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            var filteredBooks = (from b in DataController.BooksCollection
                                 where b.Name == Searchtxt.Text
                                 select b).ToList<Book>();

            BooksView.ItemsSource = filteredBooks;
            BooksView.Items.Refresh();
        }

    }
}
