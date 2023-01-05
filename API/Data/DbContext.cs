using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
namespace MovieApi.API.Data{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
            
        }
    }
}