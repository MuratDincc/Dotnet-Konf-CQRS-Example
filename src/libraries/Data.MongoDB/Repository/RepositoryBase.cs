using Data.MongoDB.Configuration;
using Data.MongoDB.Entities;
using Data.MongoDB.Repository.Abstracts;
using MongoDB.Driver;

namespace Data.MongoDB.Repository;

public class RepositoryBase<T> : IRepository<T> where T : BaseEntity
{
    protected readonly IMongoCollection<T> Collection;

    public RepositoryBase(MongoDbSettings mongoDbSettings)
    {
        var client = new MongoClient(mongoDbSettings.ConnectionString); 
        var db = client.GetDatabase(mongoDbSettings.DatabaseName);

        Collection = db.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }

    public void Delete(string id)
    {
        Collection.FindOneAndDelete(x=> x.Id == id);
    }

    public T GetById(string id)
    {
        return Collection.Find(x=> x.Id == id).FirstOrDefault();
    }

    public List<T> GetAll()
    {
        return Collection.AsQueryable().ToList();
    }

    public void Insert(T entity)
    {
        Collection.InsertOne(entity);
    }

    public void Update(T entity)
    {
        Collection.FindOneAndReplace(x=> x.Id == entity.Id, entity);
    }
}
