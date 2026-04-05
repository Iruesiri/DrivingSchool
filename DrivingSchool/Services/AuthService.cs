using DrivingSchool.Application.Interfaces;
using DrivingSchool.Domain.Entities;
using DrivingSchool.Model;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Services
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(UserDto request);
        Task<AuthResponse> LoginAsync(LoginDto request);
        Task<List<User>> GetAllAsync();
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByUserIdAsync(int id);
    }

    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtService;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository,
        IJwtTokenService jwtService, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
        
        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<User> GetByUserIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<AuthResponse> RegisterAsync(UserDto request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
                throw new Exception("User already exists");

            var user = new User
            {
                //Id = Guid.NewGuid(),
                Email = request.Email,
                Username = request.Username,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                PasswordHash =new PasswordHasher<UserDto>().HashPassword(request, request.Password),
                Role = request.Role
            };

            await _userRepository.AddAsync(user);

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);

            if (user == null || new PasswordHasher<UserDto>().VerifyHashedPassword(new UserDto { Email = user.Email, Role = user.Role }, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
                throw new Exception("Invalid credentials");

            var token = _jwtService.GenerateToken(user);

            return new AuthResponse
            {
                Token = token,
                Email = user.Email,
                Role = user.Role.ToString()
            };
        }
    }
}
