using Common.Contracts;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Common.Implementations;
public class MongoRepository<T> : IRepository<T> where T : IEntity
{
    private readonly IMongoCollection<T> dbCollection;
    private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;
    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        dbCollection = database.GetCollection<T>(collectionName);
    }

    public Task<List<T>> GetAllAsync()
    {
        return dbCollection.Find(filterBuilder.Empty).ToListAsync();
    }

    public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
    {
        return dbCollection.Find(filter).ToListAsync();
    }

    public Task<T> GetAsync(Guid id)
    {
        FilterDefinition<T> filter = filterBuilder.Eq(eEntity => eEntity.Id, id);
        return dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public Task<T> GetAsync(Expression<Func<T, bool>> filter)
    {
        return dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public Task CreateAsync(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        return dbCollection.InsertOneAsync(entity);
    }

    public Task UpdateAsync(T entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        FilterDefinition<T> filter = filterBuilder.Eq(eEntity => eEntity.Id, entity.Id);
        return dbCollection.ReplaceOneAsync(filter, entity);
    }

    public Task RemoveAsync(Guid id)
    {
        FilterDefinition<T> filter = filterBuilder.Eq(eEntity => eEntity.Id, id);
        return dbCollection.DeleteOneAsync(filter);
    }

    public Task<long> CountAsync(Expression<Func<T, bool>> filter)
    {
        return dbCollection.CountDocumentsAsync(filter);
    }
}
