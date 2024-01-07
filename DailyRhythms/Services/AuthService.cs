using DailyRhythms.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using DailyRhythms.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
namespace DailyRhythms.Services
{
	public interface IAuthService
	{
		public Task<UserDto> GetCurrentUser(string email);
		public Task<UserDto> Login(string email, string password);
		public Task<UserDto> Register(SignUpDto signUpDto);
	}
	public class AuthService : IAuthService
	{
		private readonly DailyRhythmsContext _context;
		private readonly IConfiguration _configuration;
		private readonly SymmetricSecurityKey _key;
		public AuthService(DailyRhythmsContext dbContext, IConfiguration configuration)
		{
			_context = dbContext;
			_configuration = configuration;
			_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
		}

		public async Task<UserDto> GetCurrentUser(string email)
		{	
			var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
			return new UserDto
			{
				Email = user.Email,
				Token = CreateToken(user),
				DisplayName = user.DisplayName,
				UserId = user.Id
			};
		}

		public async Task<UserDto> Login(string email, string password)
		{
			User user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
			if (user == null || BCrypt.Net.BCrypt.Verify(password, user.Password) == false)
			{
				return null;
			}

			UserDto userDto = new UserDto()
			{
				Email = user.Email,
				UserId = user.Id,
				Token = CreateToken(user),
				DisplayName = user.DisplayName
			};

			return userDto;

		}


		public async Task<UserDto> Register(SignUpDto signUpDto)
		{
			User user = new User()
			{
				DisplayName = signUpDto.DisplayName,
				Email = signUpDto.Email,
				TimeZoneId = signUpDto.TimeZoneId,
				Password = signUpDto.Password
			};

			user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			UserDto userDto = new UserDto()
			{
				Email = user.Email,
				UserId = user.Id,
				Token = CreateToken(user),
				DisplayName = user.DisplayName
			};

			return userDto;
		}

		private string CreateToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(_configuration["Token:Key"]);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Email, user.Email),
					new Claim(ClaimTypes.GivenName, user.DisplayName)
				}),
				IssuedAt = DateTime.UtcNow,
				Issuer = _configuration["Token:Issuer"],

				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature)
		};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

	}
}
