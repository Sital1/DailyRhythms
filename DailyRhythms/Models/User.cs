namespace DailyRhythms.Models
{
	public class User : BaseEntity
	{
		// Inherits 'Id', 'CreatedAt', and 'UpdatedAt' from BaseEntity
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string TimeZoneId { get; set; }  // IANA time zone identifier

		// Navigation properties
		public ICollection<ToDoItem> ToDoItems { get; set; }
		public ICollection<DailyLog> DailyLogs { get; set; }
	}
}
