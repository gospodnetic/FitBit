using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
//using MongoDB.Driver.Core;



namespace MongoConnection
{
    class BookContext
    {
        private IMongoDatabase db;

        public BookContext()
        {
            var client = new MongoClient();
            db = client.GetDatabase("bookStore");
            var collection = db.GetCollection<Book>("Book");
        }

        public IMongoCollection<Book> Books
        {
            get
            {
                return db.GetCollection<Book>("Book");
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            BookContext ctx = new BookContext();
            Book book = new Book();
            book.Title = "Petra pise po bazi";
            ctx.Books.InsertOne(book);
        }
    }

    public class Book
    {
        public ObjectId Id { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
    }
}
