using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieApi.Api.Entities;
using MovieApi.API.Entities;

namespace MovieApi.API.Repositories{
    public interface UserRepositoryInterface {
        Task<User> GetUserAsync(Guid id);

         Task<IEnumerable<User>> GetUsersAsync();

        Task CreateUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(Guid id);
    }
}