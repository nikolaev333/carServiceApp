using BaseLibrary.Responses;
using CarServiceApp.Data;
using CarServiceApp.DTO;
using CarServiceApp.Entities;
using CarServiceApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarServiceApp.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly JwtConfig _jwtConfig;

        public AuthService(AppDbContext context, IOptions<JwtConfig> jwtConfigOptions)
        {
            _context = context;
            _jwtConfig = jwtConfigOptions.Value;
        }

        public async Task<GeneralResponse> CreateAsync(RegisterDTO registerDto)
        {
            if (registerDto == null)
            {
                return new GeneralResponse(false, "Invalid registration data provided.");
            }

            var userExists = await _context.Users.AnyAsync(u =>
                u.Username == registerDto.Username || u.Email == registerDto.Email);

            if (userExists)
            {
                return new GeneralResponse(false, "Username or email already exists.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var newUser = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
               
            };

            if (!_context.Users.Any()) 
            {
                newUser.Role = UserRole.Admin;
            }
            else
            {
                newUser.Role = UserRole.Client;
            }


            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return new GeneralResponse(true, "User successfully registered.");
        }

        public async Task<AuthResponse> SignInAsync(LoginDTO loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);

            if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                var token = GenerateJwtToken(user);
                return new AuthResponse(true, "Login successful.", token);
            }

            return new AuthResponse(false, "Invalid username or password.", null);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(2), 
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
