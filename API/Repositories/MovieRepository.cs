using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieApi.API.Entities;

namespace MovieApi.API.Repositories
{
    public class MovieRepository : MovieRepositoryInterface
    {
        private readonly List<Movie> movies = new()
        {
            new Movie {Id = Guid.NewGuid(), Title = "Potion", CreatedDate = DateTimeOffset.UtcNow},
            new Movie {Id = Guid.NewGuid(), Title = "Iron Sword", CreatedDate = DateTimeOffset.UtcNow},
            new Movie {Id = Guid.NewGuid(), Title = "Bronze Shield", CreatedDate = DateTimeOffset.UtcNow},

        };
        public async Task CreateMovieAsync(Movie movie)
        {
            movies.Add(movie);
            await Task.CompletedTask;
        }

        public async Task DeleteMovieAsync(Guid id)
        {
             var index = movies.FindIndex(existingMovie => existingMovie.Id == id);
           movies.RemoveAt(index);
           await Task.CompletedTask;
        }

        public async Task<Movie> GetMovieAsync(Guid id)
        {
            var movie =  movies.Where(movies => movies.Id == id).SingleOrDefault();
            return await Task.FromResult(movie);
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
           return await Task.FromResult(movies);
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            var index = movies.FindIndex(existingMovie => existingMovie.Id == movie.Id);
            movies[index] = movie;
            await Task.CompletedTask;
        }

        Task MovieRepositoryInterface.CreateMovieAsync(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}