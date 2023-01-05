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
    [Route("actors")]
    public class ActorController: ControllerBase 
    {
        private readonly ActorRepositoryInterface repository;

        private readonly ILogger<ActorController> logger;
        
        public ActorController(ActorRepositoryInterface repository, ILogger<ActorController> logger) {
            this.repository = repository;
            this.logger = logger;
        }

        //Get Items http
        [HttpGet]
        public async Task<IEnumerable<ActorDto>> GetActorsAsync(string title = null){
             var actors = (await repository.GetActorsAsync())
                        .Select(actor => actor.AsDto());

           
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved {actors.Count()} movies");

            return actors;
        }
        [HttpGet("{id}")] //template for get function
        //ActionResult used to return multiple types
        public async Task<ActionResult<ActorDto>> GetActorAsync(Guid id){
            var actor = await repository.GetActorAsync(id);

            if(actor is null) 
            {
                return NotFound();
            }
            return actor.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ActorDto>> CreateActorAsync(CreateActorDto actorDto){
            Actor actor = new(){
                Id = Guid.NewGuid(),
                Name = actorDto.Name
              };

              await repository.CreateActorAsync(actor);
              return CreatedAtAction(nameof(GetActorAsync), new{id = actor.Id}, actor.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateActorAsync (Guid id, UpdateActorDto actorDto){
            var existingActor = await repository.GetActorAsync(id);

            if(existingActor is null){
                return NotFound();
            }

            existingActor.Name = actorDto.Name;
            
            

            await repository.UpdateActorAsync(existingActor);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteActorAsync (Guid id){
            var existingMovie = await repository.GetActorAsync(id);

            if(existingMovie is null){
                return NotFound();
            }

            await repository.DeleteActorAsync(id);
            return NoContent();
        }
    }
}