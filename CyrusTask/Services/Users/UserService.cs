using CyrusTask.DTOs.Users;
using CyrusTask.Extensions.UserDtos;
using CyrusTask.Models;
using CyrusTask.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CyrusTask.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepo;
        private readonly IConfiguration _config;

        public UserService(IGenericRepository<User> userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task<UserDto> CreateUserAsync(RegisterDto registerDTO)
        {
            var user = registerDTO.ToRegisterModel();
            user.PasswordHash = CreatePasswordHash(registerDTO.Password);

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            var userOut = user.ToDTO();

            return userOut;

        }

        public async Task<bool> CheckUserPasswordAsync(UserDto user, string password)
        {
            var userData = await _userRepo.First(u => u.Email == user.Email);

            if (userData == null || !BCrypt.Net.BCrypt.Verify(password, userData.PasswordHash))
            {
                return false;
            }

            return true;
        }

        

        public async Task<UserDto?> FindUserByEmailAsync(string email)
        {
            var user = await _userRepo.First(u => u.Email == email);
            if (user == null) 
                return null;

            var userDTO = user.ToDTO();

            return userDTO;
        }

        public async Task UpdateUserAsync(UserDto userDTO)
        {
            var user = userDTO.ToModel();
            _userRepo.Update(user);
            await _userRepo.SaveChangesAsync();
        }


        public string CreateToken(User user)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.GivenName, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));

            var token = new JwtSecurityToken(
                audience: _config["JWT:ValidAudience"],
                issuer: _config["JWT:ValidIssuer"],
                expires: DateTime.UtcNow.AddDays(double.Parse(_config["JWT:DurationInDays"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private string CreatePasswordHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            return (await _userRepo.GetAllAsync()).ToDtos().ToList();
        }
    }
}
