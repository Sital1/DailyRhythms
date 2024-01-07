using DailyRhythms.Extensions;
using DailyRhythms.Models;
using DailyRhythms.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DailyRhythms.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class DailyLogsController : ControllerBase
	{
		private readonly DailyRhythmsContext _context;

		public DailyLogsController(DailyRhythmsContext context)
		{
			_context = context;
		}

		[HttpGet("{userId}/{date}", Name = "GetDailyLog")]
		public async Task<ActionResult<DailyLogDto>> GetDailyLog(int userId, string date)
		{
			var dateOnly = DateOnly.Parse(date);
			var dailyLog = await _context.DailyLogs
				.Include(dl => dl.DailyLogToDoItems)
					.ThenInclude(dlt => dlt.ToDoItem)
						.ThenInclude(t => t.Category)
				.FirstOrDefaultAsync(dl => dl.UserId == userId && dl.Date == dateOnly);

			if (dailyLog == null)
			{
				return NotFound();
			}

			// Use the extension method to convert to DTO
			var dailyLogDto = dailyLog.ToDto();

			return dailyLogDto;
		}


		[HttpPost()]
		public async Task<ActionResult> StartDay([FromBody] UserDto userIdDto)
		{
			int userId = userIdDto.UserId;
			// check the existence of user
			var user = await _context.Users.FindAsync(userId);
			if (user == null)
			{
				return NotFound($"User with {userId} does not exist");
			}

			var userDate = Utilities.ConvertUtcToLocalTime(user.TimeZoneId).Date;
			var dateOnly = DateOnly.FromDateTime(userDate);

			// Check if a daily log already exists for the user's local today
			var dailyLog = await _context.DailyLogs
										 .FirstOrDefaultAsync(dl => dl.UserId == userId && dl.Date == dateOnly);

			if (dailyLog != null)
			{
				return BadRequest($"Daily log for {dateOnly} already exists");
			}

			if (dailyLog == null)
			{
				// Create a new DailyLog for the user's local today
				dailyLog = new DailyLog
				{
					UserId = userId,
					Date = dateOnly
				};
				_context.DailyLogs.Add(dailyLog);
				await _context.SaveChangesAsync();
			}

			// Fetch all the active ToDoItems for the user
			var activeToDoItems = await _context.ToDoItems
											.Where(ti => ti.UserId == userId &&
														 (!ti.DeletedAt.HasValue || ti.DeletedAt.Value > userDate))
											.ToListAsync();
			foreach (var item in activeToDoItems)
			{
				_context.DailyLogToDoItems.Add(new DailyLogToDoItem
				{
					DailyLogId = dailyLog.Id,
					ToDoItemId = item.Id,
					Completed = false
				});

			}
			await _context.SaveChangesAsync();
			return Ok();
		}

		[HttpPut("todoitem/toggle")]
		public async Task<ActionResult> ToggleComplete([FromBody] DailyLogToDoItemDto doItemDto)
		{
			var dailyLogToDoItem = await _context.DailyLogToDoItems
												 .FirstOrDefaultAsync(dlti => dlti.DailyLogId == doItemDto.DailyLogId &&
												 dlti.ToDoItemId == doItemDto.ToDoItemId);
			if (dailyLogToDoItem == null)
			{
				return NotFound();
			}
			dailyLogToDoItem.Completed = !dailyLogToDoItem.Completed;
			_context.Entry(dailyLogToDoItem).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!_context.DailyLogToDoItems.Any(e => e.DailyLogId == doItemDto.DailyLogId && e.ToDoItemId == doItemDto.ToDoItemId))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return NoContent();
		}

		[HttpGet("toditem/{dailyLogId}/{toDoItemId}")]
		public async Task<ActionResult<ToDoItemDto>> GetDailyLogToDoItem(int dailyLogId, int toDoItemId)
		{
			var dailyLogToDoItem = await _context.DailyLogToDoItems
				.Include(dlti => dlti.ToDoItem)
												 .FirstOrDefaultAsync(dlti => dlti.DailyLogId == dailyLogId &&
												 												 dlti.ToDoItemId == toDoItemId);

			if (dailyLogToDoItem == null)
			{
				return NotFound();
			}
			var dailyLogToDoItemDto = new ToDoItemDto
			{
				Id = dailyLogToDoItem.ToDoItemId,
				Title = dailyLogToDoItem.ToDoItem.Title,
				IsCompleted = dailyLogToDoItem.Completed
			};
			return dailyLogToDoItemDto;
		}
	}
}
