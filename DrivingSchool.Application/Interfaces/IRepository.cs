using DrivingSchool.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingSchool.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T?> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T entity);
    }
}
