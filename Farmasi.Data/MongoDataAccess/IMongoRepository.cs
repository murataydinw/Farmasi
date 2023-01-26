using System;
using System.Linq.Expressions;

namespace Farmasi.Data.MongoDataAccess
{
    public interface IMongoRepository<T>
    {
        T Get(Expression<Func<T, bool>> filter = null);
        List<T> GetList(Expression<Func<T, bool>> filter, int skip = 0, int take = 0);
        T Create(T model);
        void Update(T model);
        void Delete(Expression<Func<T, bool>> filter, T model);
        void BulkCreate(ICollection<T> models);
        void BulkDelete(Expression<Func<T, bool>> filter);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> filter, int skip = 0, int take = 0);
        Task CreateAsync(T document);
        Task UpdateAsync(T document);
        Task DeleteAsync(Expression<Func<T, bool>> filter);
        Task BulkCreateAsync(ICollection<T> documents);
        Task BulkDeleteAsync(Expression<Func<T, bool>> filter);
        long Count(Expression<Func<T, bool>> filter = null);
    }
}

