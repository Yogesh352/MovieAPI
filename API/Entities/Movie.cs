using System;

namespace MovieApi.API.Entities
{
    public class Movie
    {
        public Guid Id {get; set;}

        public string Title {get; set;}

        public string Description {get; set;}

        public DateTimeOffset CreatedDate {get; set;}
    }
}