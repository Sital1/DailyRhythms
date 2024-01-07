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
		// Inherits 'Id', 'CreatedAt', and 'UpdatedAt' from BaseEntity
		public CategoryType Id { get; set; } // Using the enum as the primary key
		public string Name { get; set; }

		// Navigation property for ToDoItems
		public ICollection<ToDoItem> ToDoItems { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
	}

}
