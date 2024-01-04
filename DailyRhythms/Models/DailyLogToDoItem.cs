namespace DailyRhythms.Models
{
	public class DailyLogToDoItem 
	{
		// Composite Key parts
		public int DailyLogId { get; set; }
		public int ToDoItemId { get; set; }

		// Navigation properties
		public DailyLog DailyLog { get; set; }
		public ToDoItem ToDoItem { get; set; }

		public bool Completed { get; set; }
	}

}