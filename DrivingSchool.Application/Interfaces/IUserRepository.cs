using DrivingSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingSchool.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
