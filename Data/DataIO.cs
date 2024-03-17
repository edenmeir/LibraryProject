using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Shapes;
using Aspose.Words.Lists;
using BookLib;
using BookLib.Logic;


namespace LiberaryProject.Data
{
    [Serializable]
    public class DataIO
    {
        private static DataIO __instance;

        public static readonly string PATH = @"C:\Programming Course\PROJECT - LIBERARY\LiberaryProject\Data\{0}";

        public static DataIO Instance
        {
            get
            {
                if (__instance == null) { __instance = new DataIO(); }
                return __instance;
            }
        }
        public ObservableCollection<Book> GetAllBooks()
        {
            string filepath = String.Format(PATH, "Books.csv");
            if (!File.Exists(filepath))
            {
                // File not exist!
                File.Create(filepath);
            }

            ObservableCollection<Book> bookList = new ObservableCollection<Book>();

            string[] lines = File.ReadAllLines(filepath);

            foreach (string line in lines)
            {
                if (line.Trim() != "")
                {
                    string[] lineData = line.Trim().Split(',');
                    bookList.Add(new Book
                    {
                        ISBN = int.Parse(lineData[0]),
                        Name = lineData[1],
                        Author = lineData[2],
                        PrintDate = DateTime.Parse(lineData[3]),
                        Edition = lineData[4],
                        Categories = lineData[5],
                        InStock = char.Parse(lineData[6]),
                        Price = float.Parse(lineData[7]),
                        Description = lineData[8].Replace("|||", Environment.NewLine)
                    });
                }
            }

            return bookList;
        }

        public void SaveBook(Book book)
        {
            string filepath = String.Format(PATH, "Books.csv");
            if (!File.Exists(filepath))
            {
                // File not exist!
                File.Create(filepath);
            }

            File.AppendAllText(filepath, $"{book.ISBN},{book.Name},{book.Author},{book.PrintDate},{book.Edition},{book.Categories},{book.InStock},{book.Price}, {book.Description.Replace(Environment.NewLine, "|||")}\n");
        }

        public Dictionary<string, string> GetAllCustomers()
        {
            string filepath = String.Format(PATH, "Customers.csv");
            if (!File.Exists(filepath))
            {
                // File not exist!
                File.Create(filepath);
            }

            Dictionary<string, string> dic = new Dictionary<string, string>();

            string[] lines = File.ReadAllLines(filepath);

            foreach (string line in lines)
            {
                if (line.Trim() != "")
                {
                    string[] lineData = line.Trim().Split(',');

                    // Retrieve the name and password from the lineData array
                    string name = lineData[0].Trim();
                    string password = lineData[1].Trim();

                    if (dic.ContainsKey(name))
                    {
                        dic[name] = password;
                    }
                    else
                    {
                        dic.Add(name, password);
                    }
                }
            }

            return dic;
        }

        public void SaveCustomer(Customer customer)
        {
            string filepath = String.Format(PATH, "Customers.csv");
            if (!File.Exists(filepath))
            {
                // File not exist!
                File.Create(filepath);
            }
            File.AppendAllText(filepath, $"{customer.Name.Trim()},{customer.Password.Trim()}\n");
        }

        public bool isCustomerExist(string name, out Customer customer)
        {
            Dictionary<string, string> customers = GetAllCustomers();

            if (customers.ContainsKey(name))
            {
                customer = new Customer(name, customers[name]);
                return true;
            }

            customer = new Customer("", "");
            return false;
        }
    }

}

