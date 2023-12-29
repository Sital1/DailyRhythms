namespace DailyRhythms.Models
{
	public class User
	{
		public int UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		// Navigation property for DailyLogs
		public ICollection<DailyLog> DailyLogs { get; set; }
	}
}
