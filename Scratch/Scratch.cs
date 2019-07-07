using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Scratch
{
    class Program
    {
        static void Main(string[] args)
        {

            //foreach (var x in Enumerable.Range(0, 10)) //.Select(i=> new BsonDocument("counter", i )))
            //{
            //    Console.WriteLine(x);

            //}

            var client = new MongoClient();
            var db = client.GetDatabase("scratch");
            var col = db.GetCollection<BsonDocument>("scratch");

            var dict = new Dictionary<string, string>() { { "a", "b" }, { "1", "2" } };
            foreach (var item in dict)
            {
                Console.WriteLine("Insert {0}:{1}", item.Key, item.Value);
                col.InsertOne(new BsonDocument(new BsonElement(item.Key, item.Value)));
            }

            var doc = new BsonDocument {{"a", "b"}, {"1", "2"}};
            Console.WriteLine(doc);
            var docElement = new BsonElement("key", "value");
            Console.WriteLine(docElement);

            var intElement = new BsonElement("age", 30);
            Console.WriteLine(intElement);

            var dateTimeElement = new BsonElement("timestamp", DateTime.UtcNow);
            Console.WriteLine(dateTimeElement);

            IFindFluent<BsonDocument, BsonDocument > cursor =  col.Find(new BsonDocument());

            Console.WriteLine("All docs");
            foreach (var i in cursor.ToEnumerable())
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
        }
    }
}
