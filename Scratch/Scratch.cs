using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Scratch
{
    class Program
    {
        static void Main(string[] args)
        {

            foreach (var x in Enumerable.Range(0, 10)) //.Select(i=> new BsonDocument("counter", i )))
            {
                Console.WriteLine(x);

            }

            Console.ReadKey();
        }
    }
}
