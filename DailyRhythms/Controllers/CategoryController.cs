using DailyRhythms.Models;
using DailyRhythms.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace DailyRhythms.Controllers
{
	[Route("api/[controller]/todoitems")]
	[ApiController]
	[Authorize]
	public class CategoryController : ControllerBase
	{
		private DailyRhythmsContext _context;

		public CategoryController(DailyRhythmsContext context)
		{
			_context = context;
		}

		[HttpGet("{userId}")]
		public async Task<ActionResult<UserCategoryDto>> AllUserToDoItems(int userId)
		{
			var user = await _context.Users.FindAsync(userId);

			if (user == null)
			{
				return BadRequest("User does not exist in db");
			}
			var toDoItems = await _context.ToDoItems
							.Where(t => t.UserId == userId
							&& !t.DeletedAt.HasValue)


							.Select(ti => new UserToDoItemDto
							{
								Id = ti.Id,
								Title = ti.Title,
								CreatedAt = Utilities.AddTimeToUtc(ti.CreatedAt, user.TimeZoneId),
								CategoryId = (int)ti.CategoryId,
								UserId = ti.UserId
							}).ToListAsync();
			var userToDoItems = new UserToDoItems
			{
				Id = userId,
				Morning = new UserCategoryDto { UserToDoItems = toDoItems.Where(ti => ti.CategoryId == (int)CategoryType.Morning).ToList() },
				Afternoon = new UserCategoryDto { UserToDoItems = toDoItems.Where(ti => ti.CategoryId == (int)CategoryType.Afternoon).ToList() },
				Evening = new UserCategoryDto { UserToDoItems = toDoItems.Where(ti => ti.CategoryId == (int)CategoryType.Evening).ToList() },
				Anytime = new UserCategoryDto { UserToDoItems = toDoItems.Where(ti => ti.CategoryId == (int)CategoryType.Anytime).ToList() }
			};

			return Ok(userToDoItems);

		}

		[HttpGet("{userId}/{categoryId}")]
		public async Task<ActionResult<IEnumerable<UserToDoItemDto>>> UserToDoItemsByCategory(int userId, int categoryId)
		{
			var user = await _context.Users.FindAsync(userId);

			if (user == null)
			{
				return BadRequest("User does not exist in db");
			}
			var toDoItems = await _context.ToDoItems
							.Where(t => t.UserId == userId && (int)t.CategoryId == categoryId
							&& !t.DeletedAt.HasValue)


							.Select(ti => new UserToDoItemDto
							{
								Id = ti.Id,
								Title = ti.Title,
								CreatedAt = Utilities.AddTimeToUtc(ti.CreatedAt, user.TimeZoneId),
								CategoryId = (int)ti.CategoryId,
								UserId = ti.UserId
							}).ToListAsync();
		

			return Ok(toDoItems);
		}



		[HttpPost]
		public async Task<ActionResult> AddToDoItem(AddToDoItemToCategoryDto addToDoItemTo)
		{
			var user = await _context.Users.FindAsync(addToDoItemTo.UserId);
			if (user == null)
			{
				return NotFound("The User does not exist");
			}
			var categoryId = (CategoryType)Enum.Parse(typeof(CategoryType), addToDoItemTo.CategoryId.ToString());
			var category = await _context.Categories.FindAsync(categoryId);
			if (category == null)
			{
				return NotFound("The Category does not exist");
			}

			var toDoItem = new ToDoItem
			{
				Title = addToDoItemTo.Title,
				CategoryId = categoryId,
				UserId = addToDoItemTo.UserId
			};

			_context.ToDoItems.Add(toDoItem);
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpPut("softdelete")]
		public async Task<ActionResult> UpdateToDoItem(DeleteToDoItemFromCategoryDto toDoItemDto)
		{
			var user = await _context.Users.FindAsync(toDoItemDto.UserId);
			if (user == null)
			{
				return NotFound("The User does not exist");
			}
			var categoryId = (CategoryType)Enum.Parse(typeof(CategoryType), toDoItemDto.CategoryId.ToString());
			var category = await _context.Categories.FindAsync(categoryId);
			if (category == null)
			{
				return NotFound("The Category does not exist");
			}


			var toDoItem = await _context.ToDoItems.FirstOrDefaultAsync(x => x.UserId == user.Id && x.CategoryId == categoryId && x.Id == toDoItemDto.Id);
			if (toDoItem == null)
			{
				return NotFound("The ToDoItem does not exist");
			}
			toDoItem.DeletedAt = Utilities.ConvertUtcToLocalTime(user.TimeZoneId);
			_context.Entry(toDoItem).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.ToDoItems.Any(x => x.UserId == user.Id && x.CategoryId == categoryId && x.Id == toDoItemDto.Id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return Ok();
		}


	}
}
