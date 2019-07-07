using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Blog
{
    class Blog
    {
        static void Main(string[] args)
        {
            var client = new MongoClient(); // mongodb://localhost:27017 is the default
            var database = client.GetDatabase("Blog");
            var users = database.GetCollection<BsonDocument>("users");

            var user_joe = new BsonDocument
            {
                {"username", "jdrumgoole"},
                {"password", "a secret"},
                {
                    "locale", new BsonDocument
                    {
                        {"lang", "EN"},
                        {"currency", "sterling"},
                    }
                },
            };

            users.InsertOne(user_joe);
            Console.WriteLine("Added user_joe");

            var user_jim = new BsonDocument
            {
                {"username", "jim"},
                {"password", "a different secret"},
            };

            var locale = new BsonDocument();
            locale.Add("lang", new BsonString("DE"));
            locale.Add("currency", new BsonString("euro"));
            user_jim.Add("locale", locale);

            users.InsertOne(user_jim);
            Console.WriteLine("Added user_jim");
            Console.ReadKey();
        }
    }
}
