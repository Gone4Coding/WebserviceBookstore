using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BookstoreWebservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IServiceBookstore
    {
        // AUTHENTICATION
        [WebInvoke(Method = "POST", UriTemplate = "/signup?token={token}")]
        [OperationContract]
        void SignUp(User user, string token); // admin only

        [WebInvoke(Method = "POST", UriTemplate = "/login?username={username}&password={password}")]
        [OperationContract]
        string LogIn(string username, string password);

        [WebInvoke(Method = "POST", UriTemplate = "/logout")]
        [OperationContract]
        void LogOut(string token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/isadmin?token={token}")]
        bool IsAdmin(string token);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/isloggedin?token={token}")]
        bool IsLoggedIn(string token);

        // GET BOOKS
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "/books?token={token}")]
        List<Book> GetBooks(string token);

        [OperationContract(Name = "GetBooksByCategory")]
        [WebInvoke(Method = "GET", UriTemplate = "/books/{category}?token={token}")]
        List<Book> GetBooks(string category, string token);

        [OperationContract(Name = "GetBookByTitle")]
        [WebInvoke(Method = "GET", UriTemplate = "/book/{title}?token={token}")]
        Book GetBook(string title, string token);

        // ADD BOOKS
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "/book?token={token}")]
        void AddBook(Book book, string token); // admin only

        // DELETE BOOKS
        [OperationContract(Name = "DeleteBookByTitle")]
        [WebInvoke(Method = "DELETE", UriTemplate = "/book/{title}?token={token}")]
        void DeleteBook(string title, string token); // admin only
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class Book
    {
        private string title;
        private string author;
        private int year;
        private double price;
        private string category;

        public Book(string title, string author, int year, double price, string category)
        {
            this.title = title;
            this.author = author;
            this.year = year;
            this.price = price;
            this.category = category;
        }

        [DataMember]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        [DataMember]
        public string Author
        {
            get { return author; }
            set { author = value; }
        }

        [DataMember]
        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        [DataMember]
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        [DataMember]
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        public override string ToString()
        {
            string res = String.Empty;
            res += "Title: " + title + Environment.NewLine;
            res += "Author: " + author + Environment.NewLine;
            res += "Year: " + year + Environment.NewLine;
            res += "Price: " + price + Environment.NewLine;
            res += "Category: " + category + Environment.NewLine;
            return res;
        }
    }

    [DataContract]
    public class User
    {
        private string username;
        private string password;
        private bool admin;

        public User(string username, string password, bool admin)
        {
            this.admin = admin;
            this.username = username;
            this.password = password;
        }

        [DataMember]
        public bool Admin
        {
            get { return admin; }
            set { admin = value; }
        }

        [DataMember]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DataMember]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
