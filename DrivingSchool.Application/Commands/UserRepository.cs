using DrivingSchool.Application.Interfaces;
using DrivingSchool.Domain.Entities;
using DrivingSchool.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Application.Commands
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
