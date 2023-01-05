using System;
using System.Collections.Generic;

namespace MovieApi.API.Entities
{
    public class Actor {
        public Guid Id {get; set;}

        public string Name {get; set;}

        public IList<Movie> Movies {get; set;}
    }
}