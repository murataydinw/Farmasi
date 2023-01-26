using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;
using Farmasi.Core.Domain;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Farmasi.Data.MongoDataAccess
{
    public class MongoRepository<T> : IMongoRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _mongoCollection;

        public MongoRepository(string connectionString)
        {
            var mongoDb = typeof(T).GetTypeInfo().GetCustomAttribute<MongoDb>();
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(mongoDb.DbName);
            _mongoCollection = database.GetCollection<T>(mongoDb.CollectionName);
        }

        public T Get(Expression<Func<T, bool>> filter = null)
        {
            return _mongoCollection.Find<T>(filter).FirstOrDefault();
        }

        public List<T> GetList(Expression<Func<T, bool>> filter, int skip = 0, int take = 0)
        {
            var query = _mongoCollection.Find(filter);
            if (skip > 0) query = query.Skip(skip);
            if (take > 0) query = query.Limit(take);

            return query.ToList();
        }
        public T Create(T model)
        {
            _mongoCollection.InsertOne(model);
            return model;
        }

        public void Update(T model)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, model.Id);
            _mongoCollection.FindOneAndReplace(filter, model);
        }

        public void Delete(Expression<Func<T, bool>> filter, T model)
        {
            _mongoCollection.DeleteOne(filter);
        }

        public void BulkCreate(ICollection<T> models)
        {
            _mongoCollection.InsertMany(models);
        }

        public void BulkDelete(Expression<Func<T, bool>> filter)
        {
            _mongoCollection.DeleteMany(filter);
        }

        public virtual Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return Task.Run(() => _mongoCollection.Find(filter).FirstOrDefaultAsync());
        }

        public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter, int skip = 0, int take = 0)
        {
            var query = _mongoCollection.Find(filter);

            if (skip > 0) query = query.Skip(skip);
            if (take > 0) query = query.Limit(take);

            return Task.Run(() => query.ToListAsync());
        }

        public virtual Task CreateAsync(T model)
        {
            return Task.Run(() => _mongoCollection.InsertOneAsync(model));
        }
        public virtual async Task UpdateAsync(T model)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, model.Id);
            await _mongoCollection.FindOneAndReplaceAsync(filter, model);
        }
        public Task DeleteAsync(Expression<Func<T, bool>> filterExpression)
        {
            return Task.Run(() => _mongoCollection.FindOneAndDeleteAsync(filterExpression));
        }
        public virtual async Task BulkCreateAsync(ICollection<T> documents)
        {
            await _mongoCollection.InsertManyAsync(documents);
        }
        public Task BulkDeleteAsync(Expression<Func<T, bool>> filter)
        {
            return Task.Run(() => _mongoCollection.DeleteManyAsync(filter));
        }

        public long Count(Expression<Func<T, bool>> filter = null)
        {
            var count = filter == null ? _mongoCollection.CountDocuments(new BsonDocument()) : _mongoCollection.CountDocuments(filter);
            return count;
        }
    }
}

