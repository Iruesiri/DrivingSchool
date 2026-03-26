using DrivingSchool.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace DrivingSchool.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
}
