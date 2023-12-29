using DailyRhythms.Models;
using DailyRhythms.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace DailyRhythms.Controllers
{
	[Route("api/[controller]")]

	public class UsersController : ControllerBase
	{
		private readonly DailyRythmsContext _context;

		public UsersController(DailyRythmsContext context)
		{
			_context = context;
		}

		[HttpPost]
		// write a method for creating a user using SignUpDto
		public async Task<ActionResult<User>> CreateUser([FromBody]SignUpDto signUpDto)
		{
			var user = new User
			{
				FirstName = signUpDto.FirstName,
				LastName = signUpDto.LastName
			};

			_context.Users.Add(user);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetUser", new { id = user.UserId }, user);
		}

		[HttpGet("{id}",Name ="GetUser")]
		public async Task<ActionResult<User>> GetUser(int id)
		{
			var user = await _context.Users.FindAsync(id);

			if (user == null)
			{
				return NotFound();
			}

			UserResponseDto userResponseDto= new UserResponseDto
			{
				UserId = user.UserId,
				FirstName = user.FirstName,
				LastName = user.LastName
			};

			return user;
		}

	}
}
