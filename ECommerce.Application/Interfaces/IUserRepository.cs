using ECommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User?> GetByIdAsync(long userId);
        Task<User?> GetByVerificationTokenAsync(string token);
        Task AddUserAsync(User user);
        Task UpdateAsync(User user);
    }
}


