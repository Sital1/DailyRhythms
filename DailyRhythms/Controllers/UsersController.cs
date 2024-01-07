using DailyRhythms.Models;
using DailyRhythms.Models.Dtos;
using DailyRhythms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace DailyRhythms.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class UsersController : ControllerBase
	{
		private readonly DailyRhythmsContext _context;
		private readonly IAuthService _authService;

		public UsersController(DailyRhythmsContext context, IAuthService authService)
		{
			_context = context;
			_authService = authService;
		}

		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login([FromBody]LoginDto loginDto)
		{
			UserDto loggedInUser = await _authService.Login(loginDto.Email, loginDto.Password);

			if (loggedInUser != null)
			{
				return Ok(loggedInUser);
			}
			return BadRequest(new { message = "User login unsuccessful" });
		}


		[HttpGet("emailexists")]
		public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
		{
			return await _context.Users.FirstOrDefaultAsync(u => u.Email == email) != null;
		}


		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(SignUpDto registerDto)
		{
			if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
			{
				return new BadRequestObjectResult("Email address is in use");
			}

			var user = await _authService.Register(registerDto);

			if (user == null) return BadRequest(new { message = "User login unsuccessful" });

			return Ok(user);
		}


		[Authorize(AuthenticationSchemes = "Bearer")]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{	
			var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
			UserDto userDto = await _authService.GetCurrentUser(email);
			return Ok(userDto);
		}

	}
}
