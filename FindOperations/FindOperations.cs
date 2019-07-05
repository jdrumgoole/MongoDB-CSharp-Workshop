using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace FindOperations
{
    class FindOperations
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;

        public FindOperations(string mongoDBURI, string databaseName, string collectionName)
        {
            client = new MongoClient();
            database = client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);
        }

        public IMongoCollection<BsonDocument> Find(BsonDocument doc)
        {
            return collection.Find(doc);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
