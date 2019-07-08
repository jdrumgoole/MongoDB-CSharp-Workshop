using System;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ManyArticles
{
    class Blog
    {
        private readonly MongoClient client;
        private readonly IMongoDatabase database;
        public  IMongoCollection<BsonDocument> Users;
        public  IMongoCollection<BsonDocument> Articles;
        public Blog()
        {
            client = new MongoClient();
            // mongodb://localhost:27017 is the default

            database = client.GetDatabase("blog");
            Users = database.GetCollection<BsonDocument>("users");
            Articles = database.GetCollection<BsonDocument>("articles");
        }

        public void Clear()
        {
            database.DropCollection("users");
            database.DropCollection("articles");          
            Users = database.GetCollection<BsonDocument>("users");
            Articles = database.GetCollection<BsonDocument>("articles");


        }
        public void AddArticle(
            string authorUsername,
            string title,
            string body,
            string[] tags)
        {

            var article = new BsonDocument
            {
                {"Title",title },
                {"Body", body },
                {"Author", authorUsername},
                {"Posting date", DateTime.UtcNow },
                {"Tags", new BsonArray(tags)}
            };

            var doc = Users.Find(new BsonDocument
                {{"username", authorUsername}}).FirstOrDefault();

            if (doc is null)
            {
                Console.WriteLine("Cannot post article: No such author: {0}", authorUsername);
                throw new InvalidOperationException("No User such user" + authorUsername );
            }
            else
            {
                Articles.InsertOne(article);
            }
        }

        
    }
}