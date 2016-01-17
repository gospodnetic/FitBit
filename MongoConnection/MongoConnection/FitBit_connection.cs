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

            var combinedJson = JsonConvert.SerializeObject(new
            {
                obj1 = JObject.Parse(line1),
                obj2 = JObject.Parse(line2)
            });

            MongoDB.Bson.BsonDocument document = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(combinedJson);
            FitBitContext ctx = new FitBitContext();
            //if (ctx.Users.Find<BsonDocument> document["user"]["age"].AsString)
            //FilterDefinition<BsonDocument> filter = "{ age: 2 }";
            //var found = ctx.Users.Find<BsonDocument>(filter);
            var filter = Builders<BsonDocument>.Filter.Eq("age", "28");
            var result = ctx.Users.Find(filter).ToListAsync();
            // NE RADI
            Console.WriteLine(result);
            ctx.Users.InsertOne(document);
        }
    }
}
