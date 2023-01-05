using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieApi.API.Entities;

namespace MovieApi.API.Repositories{
    public interface ActorRepositoryInterface {
        Task<Actor> GetActorAsync(Guid id);

         Task<IEnumerable<Actor>> GetActorsAsync();

        Task CreateActorAsync(Actor actor);

        Task UpdateActorAsync(Actor actor);

        Task DeleteActorAsync(Guid id);
    }
}