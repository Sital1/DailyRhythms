namespace DailyRhythms.Models
{
	public enum CategoryType
	{
		Morning = 1,
		Afternoon,
		Evening,
		Anytime
	}

	public class Category
	{
		public CategoryType Id { get; set; } // Using the enum as the primary key
		public string Name { get; set; }

		// Navigation property for Tasks
		public ICollection<ToDoItem> ToDoItems { get; set; }
	}
}
