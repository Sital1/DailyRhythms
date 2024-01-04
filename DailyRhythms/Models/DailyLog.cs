namespace DailyRhythms.Models
{
	public class DailyLog : BaseEntity
	{
		// Inherits 'Id', 'CreatedAt', and 'UpdatedAt' from BaseEntity
		public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

		// Foreign keys and Navigation properties
		public int UserId { get; set; }
		public User User { get; set; }
		public ICollection<DailyLogToDoItem> DailyLogToDoItems { get; set; }
	}

}