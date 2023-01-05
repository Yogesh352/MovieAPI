using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieApi.API.Entities;

namespace MovieApi.API.Repositories{
    public class MongoDbActorsRepository : ActorRepositoryInterface
    {
        private const string databaseName = "MovieAPI";
        private const string collectionName = "actors";
        private readonly IMongoCollection<Actor> actorsCollection;

        private readonly FilterDefinitionBuilder<Actor> filterBuilder = Builders<Actor>.Filter;
        public MongoDbActorsRepository(IMongoClient mongoClient){
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            actorsCollection = database.GetCollection<Actor>(collectionName);
        }
        public async Task CreateActorAsync(Actor actor)
        {
            await actorsCollection.InsertOneAsync(actor);
        }

        public async Task DeleteActorAsync(Guid id)
        {
            var filter = filterBuilder.Eq(actor => actor.Id, id );
            await actorsCollection.DeleteOneAsync(filter);
        }

        public async Task<Actor> GetActorAsync(Guid id)
        {
               var filter = filterBuilder.Eq(actor => actor.Id, id);
                return await actorsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Actor>> GetActorsAsync()
        {
             return await actorsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateActorAsync(Actor actor)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, actor.Id);
            await actorsCollection.ReplaceOneAsync(filter, actor);
        }
    }
}