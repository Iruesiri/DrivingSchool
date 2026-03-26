namespace DrivingSchool.Model
{
    public class AuthResponse : Response
    {
        public string Token { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
