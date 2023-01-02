using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApi.Api;
using MovieApi.API.Dtos;
using MovieApi.API.Entities;
using MovieApi.API.Repositories;

namespace MovieApi.API.Controllers {
    [ApiController]
    [Route("movies")]
    public class MovieController: ControllerBase 
    {
        private readonly MovieRepositoryInterface repository;

        private readonly ILogger<MovieController> logger;
        
        public MovieController(MovieRepositoryInterface repository, ILogger<MovieController> logger) {
            this.repository = repository;
            this.logger = logger;
        }

        //Get Items http
        [HttpGet]
        public async Task<IEnumerable<MovieDto>> GetMoviesAsync(string title = null){
             var movies = (await repository.GetMoviesAsync())
                        .Select(movie => movie.AsDto());

           
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {movies.Count()} movies");

            return movies;
        }
        [HttpGet("{id}")] //template for get function
        //ActionResult used to return multiple types
        public async Task<ActionResult<MovieDto>> GetMovieAsync(Guid id){
            var movie = await repository.GetMovieAsync(id);

            if(movie is null) 
            {
                return NotFound();
            }
            return movie.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<MovieDto>> CreateMovieAsync(CreateMovieDto movieDto){
            Movie movie = new(){
                Id = Guid.NewGuid(),
                Title = movieDto.Title,
                Description = movieDto.Description,
                
                CreatedDate = DateTimeOffset.UtcNow
                
              };

              await repository.CreateMovieAsync(movie);
              return CreatedAtAction(nameof(GetMovieAsync), new{id = movie.Id}, movie.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMovieAsync (Guid id, UpdateMovieDto movieDto){
            var existingMovie = await repository.GetMovieAsync(id);

            if(existingMovie is null){
                return NotFound();
            }

            existingMovie.Title = movieDto.Title;
            existingMovie.Description = movieDto.Description;
            

            await repository.UpdateMovieAsync(existingMovie);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovieAsync (Guid id){
            var existingMovie = await repository.GetMovieAsync(id);

            if(existingMovie is null){
                return NotFound();
            }

            await repository.DeleteMovieAsync(id);
            return NoContent();
        }
    }
}