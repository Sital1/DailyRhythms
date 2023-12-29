namespace DailyRhythms.Models
{
	public class ToDoItem
	{
		public int Id { get; set; }
		public string Title { get; set; }

		// Foreign key for Category
		public CategoryType CategoryId { get; set; }
		public Category Category { get; set; }

		// Navigation property for DailyLogTasks
		public ICollection<DailyLogToDoItem> DailyLogTasks { get; set; }
	}
}
