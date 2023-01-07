using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepository
{
    public interface IMongoContext
    {
        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }

    }
}
