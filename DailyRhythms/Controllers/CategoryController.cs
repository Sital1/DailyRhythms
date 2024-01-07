using DailyRhythms.Models;
using DailyRhythms.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DailyRhythms.Controllers
{
	[Route("api/[controller]/todoitem")]
	[ApiController]
	[Authorize]
	public class CategoryController : ControllerBase
	{
		private DailyRhythmsContext _context;

		public CategoryController(DailyRhythmsContext context)
		{
			_context = context;
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

		
			var toDoItem = await _context.ToDoItems.FirstOrDefaultAsync(x=> x.UserId == user.Id && x.CategoryId == categoryId && x.Id == toDoItemDto.Id);
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
