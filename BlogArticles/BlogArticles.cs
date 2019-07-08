using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;


namespace BlogArticles
{
    class BlogArticles
    {
        static IMongoCollection<BsonDocument> AddUsers(IMongoDatabase database)
        {
            var users = database.GetCollection<BsonDocument>("users");
            var userJoe = new BsonDocument
            {
                {"username", "jdrumgoole"},
                {"password", "a secret"},
                {"age", 55 },
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
                {"age", 42 }
            };

            var locale = new BsonDocument();
            locale.Add("lang", "DE");
            locale.Add("currency", "euro");
            locale.Add("id", 524);
            userJim.Add("locale", locale);

            users.InsertOne(userJim);

            Console.WriteLine("Added user_jim");

            return users;
        }
        static void Main(string[] args)
        {
            var client = new MongoClient();
            // mongodb://localhost:27017 is the default

            var database = client.GetDatabase("Blog");

            var users = AddUsers(database);

            var articles = database.GetCollection<BsonDocument>("articles");

            var authorUsername = "jdrumgoole";

            var article = new BsonDocument
            {
                {"Title", "Gettysburg Address" },
                {"Body", "Four score and seven years ago our fathers brought forth on this continent, a new nation" },
                {"Author", authorUsername},
                {"Posting date", DateTime.UtcNow },
                {"Tags", new BsonArray{"politics", "Lincoln", "America"}}
            };

            var doc = users.Find(new BsonDocument
                {{"username", authorUsername}}).FirstOrDefault();

            if (doc is null)
            {
                Console.WriteLine("Cannot post article: No such author: {0}", authorUsername);
            }
            else
            {
                articles.InsertOne(article);
                Console.WriteLine("Article posted");
            }
            Console.ReadKey();
        }
    }
}
