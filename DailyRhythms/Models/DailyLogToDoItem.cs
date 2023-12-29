namespace DailyRhythms.Models
{
	public class DailyLogToDoItem
	{
		public int DailyLogId { get; set; }
		public int ToDoItemId { get; set; }

		// Navigation properties
		public DailyLog DailyLog { get; set; }
		public ToDoItem ToDoItem { get; set; }

		public bool Completed { get; set; }
	}
}