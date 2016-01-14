using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;


namespace MongoConnection
{
    class FitBitContext
    {
        private IMongoDatabase db;

        public FitBitContext()
        {
            var client = new MongoClient();
            db = client.GetDatabase("FitBit");
            var collection = db.GetCollection<BsonDocument>("Users");
        }

        public IMongoCollection<BsonDocument> Users
        {
            get
            {
                return db.GetCollection<BsonDocument>("Users");
            }
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            string line;
            using (StreamReader reader = new StreamReader("user.json"))
            {
                line = reader.ReadToEnd();
            }

            MongoDB.Bson.BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(line);
            FitBitContext ctx = new FitBitContext();
            ctx.Users.InsertOne(document);
        }
    }
}
