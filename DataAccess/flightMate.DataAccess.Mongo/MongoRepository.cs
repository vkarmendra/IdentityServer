using System;
using System.Collections.Generic;
using flightMate.DataAccess.Abstraction;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace flightMate.DataAccess.Mongo
{
    public class MongoRepository : IRepository
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public MongoRepository(string connectionString,string database)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(database);
        }

        public IQueryable<T> All<T>() where T : class, new()
        {
            return _database.GetCollection<T>(typeof(T).Name).AsQueryable();
        }

        public IQueryable<T> Where<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            return All<T>().Where(expression);
        }

        public async Task Delete<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : class, new()
        {
            await _database.GetCollection<T>(typeof(T).Name).DeleteManyAsync(predicate);
        }
        public async Task<T> Single<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : class, new()
        {
            return (await _database.GetCollection<T>(typeof(T).Name).FindAsync(expression)).SingleOrDefault();
        }

        public async Task<bool> CollectionExists<T>() where T : class, new()
        {
            var collection = _database.GetCollection<T>(typeof(T).Name);
            var filter = new BsonDocument();
            var totalCount = await collection.CountDocumentsAsync(filter);
            return (totalCount > 0) ? true : false;

        }

        public async Task Add<T>(T item) where T : class, new()
        {
            await _database.GetCollection<T>(typeof(T).Name).InsertOneAsync(item);
        }

        public async Task Add<T>(IEnumerable<T> items) where T : class, new()
        {
            await _database.GetCollection<T>(typeof(T).Name).InsertManyAsync(items);
        }
       
    }
}
