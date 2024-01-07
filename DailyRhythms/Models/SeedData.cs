using System.Reflection;
using System.Text.Json;

namespace DailyRhythms.Models
{
	public class SeedData
	{
		public static async Task SeedAsync(DailyRhythmsContext context)
		{
			string passwordHash = BCrypt.Net.BCrypt.HashPassword("Pa$$w0rd");

			// Users
			var users = new List<User>
			{
				new User { DisplayName = "John Doe", Email = "john.doe@example.com", TimeZoneId = "America/New_York", Password = passwordHash },
				new User { DisplayName = "Sita Sharma", Email = "sita.sharma@example.com", TimeZoneId = "Asia/Kathmandu", Password = passwordHash }
			};
			if (!context.Users.Any())
			{
				context.Users.AddRange(users);
				if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
				var toDoItems = new List<ToDoItem>();
				var categories = Enum.GetValues(typeof(CategoryType)).Cast<CategoryType>();
			
				foreach (var user in context.Users.ToList())
				{
					foreach (var category in categories)
					{
						toDoItems.Add(new ToDoItem { Title = $"Task 1 for {category}", UserId = user.Id, CategoryId = category });
						toDoItems.Add(new ToDoItem { Title = $"Task 2 for {category}", UserId = user.Id, CategoryId = category });
					}
					if (!context.ToDoItems.Any(t => t.UserId == user.Id))
					{
						context.ToDoItems.AddRange(toDoItems);
						
					}
				}

			}

			if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
		}
	}
}
