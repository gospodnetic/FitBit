using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft;


namespace MongoConnection
{
    class FitBitContext
    {
        private IMongoDatabase db;

        public FitBitContext()
        {
            var client = new MongoClient();
            db = client.GetDatabase("FitBit");
            var collection = db.GetCollection<User>("Users");
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return db.GetCollection<User>("Users");
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {


            FitBitContext ctx = new FitBitContext();
            User user = new User();
            user.Name = "Petra";
            user.LastName = "Gospodnetić";
            user.Age = "24";
            ctx.Users.InsertOne(user);
        }
    }

    public class User
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string  { get; set; }
        public string Age { get; set; }
        public string Age { get; set; }
        public string Age { get; set; }
        public string Age { get; set; }
    }
}
