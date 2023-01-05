using MovieApi.API.Dtos;
using MovieApi.API.Entities;

namespace MovieApi.Api {
    public static class Extensions{
        public static MovieDto AsDto(this Movie movie){
             return new MovieDto (movie.Id, movie.Title, movie.Description, movie.CreatedDate);
            
        }

        public static ActorDto AsDto(this Actor actor){
             return new ActorDto (actor.Id, actor.Name);
            
        }
    }
}