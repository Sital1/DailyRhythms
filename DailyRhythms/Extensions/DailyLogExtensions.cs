using DailyRhythms.Models;
using DailyRhythms.Models.Dtos;

namespace DailyRhythms.Extensions
{
	public static class DailyLogExtensions
	{
		public static DailyLogDto ToDto(this DailyLog dailyLog)
		{
			return new DailyLogDto
			{	
				Id = dailyLog.Id,
				Date = dailyLog.Date,
				Morning = new CategoryToDoItemDto { ToDoItems = dailyLog.GetToDoItemsByCategory(CategoryType.Morning) },
				Afternoon = new CategoryToDoItemDto { ToDoItems = dailyLog.GetToDoItemsByCategory(CategoryType.Afternoon) },
				Evening = new CategoryToDoItemDto { ToDoItems = dailyLog.GetToDoItemsByCategory(CategoryType.Evening) },
				Anytime = new CategoryToDoItemDto { ToDoItems = dailyLog.GetToDoItemsByCategory(CategoryType.Anytime) }
			};
		}

		private static List<ToDoItemDto> GetToDoItemsByCategory(this DailyLog dailyLog, CategoryType categoryType)
		{
			return dailyLog.DailyLogToDoItems
				.Where(dlt => dlt.ToDoItem.Category.Id == categoryType) // Assuming 'Type' is the correct field
				.Select(dlt => new ToDoItemDto {Id=dlt.ToDoItem.Id, Title = dlt.ToDoItem.Title, IsCompleted = dlt.Completed })
				.ToList();
		}
	}
}
