using DrivingSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingSchool.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}
