using System;
using System.ComponentModel.DataAnnotations;

namespace MovieApi.Api.Entities{
    public class User 
    {
        public Guid Id {get; set;}
        [Required]
        public string Name { get; set;}

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email {get; set;}

        [Required]
        public string Password{get; set;}
    }
}