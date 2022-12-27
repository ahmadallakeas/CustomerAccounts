using Application.Configurations;
using Application.Interfaces.IRepository;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoInfrastructure.Repository
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        public string CustomersName;
        public string AccountsName;

        public MongoContext(IOptions<MongoDbConfiguration> mongoOptions)
        {
            var config = mongoOptions.Value;
            _client = new MongoClient(config.ConnectionString);
            _database = _client.GetDatabase(config.DatabaseName);
            CustomersName = config.CustomersCollectionName;
            AccountsName = config.AccountsCollectionName;
        }
        public IMongoClient Client => _client;
        public IMongoDatabase Database => _database;
    }
}
