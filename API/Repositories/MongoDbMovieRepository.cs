using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieApi.API.Entities;

namespace MovieApi.API.Repositories{
    public class MongoDbMoviesRepository : MovieRepositoryInterface
    {
        private const string databaseName = "MovieAPI";
        private const string collectionName = "movies";
        private readonly IMongoCollection<Movie> moviesCollection;

        private readonly FilterDefinitionBuilder<Movie> filterBuilder = Builders<Movie>.Filter;
        public MongoDbMoviesRepository(IMongoClient mongoClient){
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            moviesCollection = database.GetCollection<Movie>(collectionName);
        }
        
        public async Task CreateMovieAsync(Movie movie)
        {
            await moviesCollection.InsertOneAsync(movie);
        }

        public async Task DeleteMovieAsync(Guid id)
        {
            var filter = filterBuilder.Eq(movie => movie.Id, id );
            await moviesCollection.DeleteOneAsync(filter);
        }

        public async Task<Movie> GetMovieAsync(Guid id)
        {
               var filter = filterBuilder.Eq(movie => movie.Id, id);
                return await moviesCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
             return await moviesCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            var filter = filterBuilder.Eq(existingItem => existingItem.Id, movie.Id);
            await moviesCollection.ReplaceOneAsync(filter, movie);
        }
    }
}