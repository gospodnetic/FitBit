using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
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
            string line1;
            using (StreamReader reader = new StreamReader("user.json"))
            {
                line1 = reader.ReadToEnd();
            }
            string line2;
            using (StreamReader reader = new StreamReader("user2.json"))
            {
                line2 = reader.ReadToEnd();
            }

            var a = JsonConvert.SerializeObject(new
            {
                obj1 = JObject.Parse(line1),
                obj2 = JObject.Parse(line2)
            });
            MongoDB.Bson.BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(a);
            FitBitContext ctx = new FitBitContext();
            ctx.Users.InsertOne(document);
        }
    }
}
