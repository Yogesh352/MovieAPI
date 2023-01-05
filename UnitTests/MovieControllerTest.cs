using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MovieApi.API.Controllers;
using MovieApi.API.Dtos;
using MovieApi.API.Entities;
using MovieApi.API.Repositories;
using Xunit;

namespace MovieApi.UnitTests
{
    public class MovieControllerTests
    {
        private readonly Mock<MovieRepositoryInterface> repositoryStub = new();
        private readonly Mock<ILogger<MovieController>> loggerStub = new();
        private readonly Random rand = new();
        [Fact]
        public async Task GetMovieAsync_WithNonExistingMovie_ReturnsNotFound()
        {
            repositoryStub.Setup( repo => repo.GetMovieAsync(It.IsAny<Guid>())).ReturnsAsync((Movie)null);
            var controller = new MovieController(repositoryStub.Object, loggerStub.Object);

            //Act
            var result = await controller.GetMovieAsync(Guid.NewGuid());

            //Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

          [Fact]
        public async Task GetMovieAsync_WithExistingMovie_ReturnsExpectedMovie()
        {
            // Arrange
            Movie expectedItem = CreateRandomMovie();
            repositoryStub.Setup(repo => repo.GetMovieAsync(It.IsAny<Guid>()))
                .ReturnsAsync(expectedItem);

            var controller = new MovieController(repositoryStub.Object, loggerStub.Object);
            // Act
            var result = await controller.GetMovieAsync(Guid.NewGuid());
            // Assert
            result.Value.Should().BeEquivalentTo(expectedItem);
           

        }

         [Fact]
        public async Task GetMoviesAsync_WithExistingMovies_ReturnsAllMovies()
        {
            var expectedItems = new[] {CreateRandomMovie(), CreateRandomMovie(), CreateRandomMovie()};

            repositoryStub.Setup( repo => repo.GetMoviesAsync()).ReturnsAsync(expectedItems);
            var controller = new MovieController(repositoryStub.Object, loggerStub.Object);

            //Act
            var actualItems = await controller.GetMoviesAsync();

            //Assert
            actualItems.Should().BeEquivalentTo(expectedItems);
        }
        [Fact]
        public async Task CreateMovieAsync_WithMovieToCreate_ReturnsCreatedMovie()
        {
          var itemToCreate = new CreateMovieDto(
              Guid.NewGuid().ToString(), 
              Guid.NewGuid().ToString()
              );
          

            
            var controller = new MovieController(repositoryStub.Object, loggerStub.Object);

            //Act
           var result = await controller.CreateMovieAsync(itemToCreate);

            //Assert
            var createdItem = (result.Result as CreatedAtActionResult).Value as MovieDto;
            itemToCreate.Should().BeEquivalentTo(
                createdItem,
                options => options.ComparingByMembers<MovieDto>().ExcludingMissingMembers()
            );
            createdItem.Id.Should().NotBeEmpty();
            createdItem.CreatedDate.Should().BeCloseTo(DateTime.UtcNow,  TimeSpan.FromSeconds(1000));
        }

        [Fact]
        public async Task UpdateMovieAsync_WithExistingMovie_ReturnsNoContent()
        {
          
            var existingItem = CreateRandomMovie();
            repositoryStub.Setup( repo => repo.GetMovieAsync(It.IsAny<Guid>())).ReturnsAsync(existingItem);

            var itemId = existingItem.Id;

            var itemToUpdate = new UpdateMovieDto(
                Guid.NewGuid().ToString(), 
                Guid.NewGuid().ToString()
      
                );

            var controller = new MovieController(repositoryStub.Object, loggerStub.Object);

            //Act
           var result = await controller.UpdateMovieAsync(itemId, itemToUpdate);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task DeleteMovieAsync_WithExistingMovie_ReturnsNoContent()
        {
          
            var existingItem = CreateRandomMovie();
            repositoryStub.Setup( repo => repo.GetMovieAsync(It.IsAny<Guid>())).ReturnsAsync(existingItem);
            var controller = new MovieController(repositoryStub.Object, loggerStub.Object);

            //Act
           var result = await controller.DeleteMovieAsync(existingItem.Id);

            //Assert
            result.Should().BeOfType<NoContentResult>();
        }




        private Movie CreateRandomMovie()
        {
           return new() 
            {
                Id = Guid.NewGuid(),
                Title = Guid.NewGuid().ToString(),
                Description =  Guid.NewGuid().ToString(),
                CreatedDate = DateTimeOffset.UtcNow
            };
        }
    }
}
