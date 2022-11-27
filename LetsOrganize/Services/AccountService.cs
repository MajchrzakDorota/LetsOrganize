using LetsOrganize.Entities;
using LetsOrganize.Interfaces;
using LetsOrganize.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LetsOrganize.Services
{
    public class AccountService : IAccountService
    {
        private readonly LetsOrganizeDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly ILogger<AccountService> _logger;

        public AccountService(LetsOrganizeDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, ILogger<AccountService> logger)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _logger = logger;
        }

        public async Task<bool> RegisterUser(UserDto userDto)
        {
            var newUser = new User()
            {
                Email = userDto.Email,
                Name = userDto.Name
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, userDto.Password);

            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message);
            }

            return true;
        }

        public async Task<string> GenerateJwtToken(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null)
            {
                throw new ArgumentException("Invalid username or password");
            }

            var passwordVerifiedResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (passwordVerifiedResult == PasswordVerificationResult.Failed)
            {
                throw new ArgumentException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            return await Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}
