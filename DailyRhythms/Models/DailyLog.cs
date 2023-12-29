namespace DailyRhythms.Models
{
	public class DailyLog
	{
		public int Id { get; set; }
		public DateTime Date { get; set; }

		// Foreign key for User
		public int UserId { get; set; }
		public User User { get; set; }

		// Navigation property for DailyLogTasks
		public ICollection<DailyLogToDoItem> DailyLogToDoItems { get; set; }
	}
}