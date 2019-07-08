using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ManyArticles
{
    class ManyArticles
    {
        // Generate a random string with a given size

        private readonly Random random;

        public ManyArticles()
        {
            random = new Random();
        }
        public string RandomString(int size, bool lowerCase=true)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public BsonDocument MakeArticle(int count)
        {
            return new BsonDocument
            {
                {"_id", count },
                {"title", "title" + count },
                {"authorName", "user" + count },
                {"body",  RandomString(20)},
                {"post date", DateTime.UtcNow }
            };
        }

        public BsonDocument MakeUser(int count)
        {
            return new BsonDocument
            {
                {"_id", "user" + count},
                {"password", RandomString(10) },
                {"karma", random.Next(0,500) },
                {"created", DateTime.UtcNow},
                {"lang", "EN" }
            };
        }


        static void Main(string[] args)
        {

            var blog = new Blog();
            const int userCount = 10000;
            const int articleCount = 10000;
            List<BsonDocument> users = new List<BsonDocument>();
            List<BsonDocument> articles = new List<BsonDocument>();
            
            ManyArticles makeDocs = new ManyArticles();

            blog.Clear();
            foreach (var i in Enumerable.Range(1, userCount))
            {
                users.Add(makeDocs.MakeUser(i));

                if ((i % 500) == 0)
                {
                    blog.Users.InsertMany(users);
                    Console.WriteLine("Inserted {0} users", i);
                    users.Clear();
                }
            }

            foreach (var i in Enumerable.Range(1, articleCount))
            {
                articles.Add(makeDocs.MakeArticle(i));
                if ((i % 500) == 0)
                {
                    blog.Articles.InsertMany(articles);
                    Console.WriteLine("Inserted {0} articles", i);
                    articles.Clear();
                }
            }
            Console.ReadKey();
        }
    }
}
