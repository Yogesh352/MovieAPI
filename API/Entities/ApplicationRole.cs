
using System;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace MovieApi.API.Entities
{
      [CollectionName("Roles")]
    public class ApplicationRole: MongoIdentityRole<Guid>
    {
        
    }
}