using DrivingSchool.Domain.Enum;
using System.Data;

namespace DrivingSchool.Model
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string Password { get; set; }
    }
}
