using System;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.API.Dtos
{
    public record MovieDto(Guid Id, string Title, string Description, DateTimeOffset CreatedDate);
    public record CreateMovieDto([Required] string Title, string Description);
    public record UpdateMovieDto([Required] string Title, string Description);
}