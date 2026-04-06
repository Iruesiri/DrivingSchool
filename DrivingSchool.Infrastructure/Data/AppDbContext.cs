using DrivingSchool.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingSchool.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
