using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MongoDB.Driver;
using System.Configuration;


namespace OCCEjecutivoAPI.Models
{
    public class DataContext
    {

        public static readonly IMongoClient _client;
        public static readonly IMongoDatabase _database;

        static DataContext()
        {
            _client = new MongoClient(string.Format("mongodb://{0}:{1}",
                ConfigurationManager.AppSettings["MongoServer"],
                ConfigurationManager.AppSettings["MongoPort"]));

            _database = _client.GetDatabase(ConfigurationManager.AppSettings["Database"]);
        }

        public IMongoClient Client
        {
            get { return _client; }
        }

        public IMongoCollection<Candidate> Candidates_Data
        {
            get { return _database.GetCollection<Candidate>("Candidates"); }
        }



    }
}