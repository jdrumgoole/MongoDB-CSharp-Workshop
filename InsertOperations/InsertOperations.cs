using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

namespace InsertOperations
{
    class InsertOperations
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;

        public InsertOperations(string mongoDBURI, string databaseName, string collectionName)
        {
            client = new MongoClient();
            database = client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);
        }

        public void InsertOne(BsonDocument doc)
        {
            collection.InsertOne(doc);
        }

        public void InsertMany(params BsonDocument[] docs)
        {
            collection.InsertMany(docs);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var insertOps = new InsertOperations("mongodb://localhost:27017", "test1", "test");
            insertOps.InsertOne( new BsonDocument{{"Hello", "World"}, {"ts", DateTime.Now}});
            Console.WriteLine("InsertOne");


            insertOps.InsertMany(new BsonDocument { { "Goodbye", "Earth" }, { "ts", DateTime.Now } },
                                 new BsonDocument { { "Hello", "Mars" }, { "ts", DateTime.Now } },
                                 new BsonDocument { { "Goodbye", "Mars" }, { "ts", DateTime.Now } });
            Console.WriteLine("InsertMany");
            Console.ReadKey();
        }
    }
}
