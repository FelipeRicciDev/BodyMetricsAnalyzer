//using Domain.Entities;

//namespace Infrastructure.Data.Mongo.Repositories.v1.Entity;

//public class EntityRepository(string collectionName) : MongoDbBaseRepository<BodyMetricsAnalyzerEntity>(collectionName), IEntityRepository
//{
//    private readonly string _collection = collectionName;
//    public async Task AddAsync(BodyMetricsAnalyzerEntity entity)
//    {
//        var collection = Database.GetCollection<BodyMetricsAnalyzerEntity>(_collection);
//        await collection.InsertOneAsync(entity);
//    }
//}
