using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieApi.API.Entities;

namespace MovieApi.API.Repositories{
    public interface MovieRepositoryInterface {
        Task<Movie> GetMovieAsync(Guid id);

         Task<IEnumerable<Movie>> GetMoviesAsync();

        Task CreateMovieAsync(Movie movie);

        Task UpdateMovieAsync(Movie movie);

        Task DeleteMovieAsync(Guid id);
    }
}