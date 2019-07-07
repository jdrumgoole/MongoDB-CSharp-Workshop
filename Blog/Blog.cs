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

            var userJoe = new BsonDocument
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

            users.InsertOne(userJoe);
            Console.WriteLine("Added user_joe");

            var userJim = new BsonDocument
            {
                {"username", "jim"},
                {"password", "a different secret"},
            };

            var locale = new BsonDocument();
            locale.Add("lang", "DE");
            locale.Add("currency","euro");
            userJim.Add("locale", locale);

            users.InsertOne(userJim);
            Console.WriteLine("Added user_jim");
            Console.ReadKey();
        }
    }
}
