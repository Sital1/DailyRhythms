namespace DailyRhythms.Models
{
	public class ToDoItem : BaseEntity
	{
		// Inherits 'Id', 'CreatedAt', and 'UpdatedAt' from BaseEntity
		public string Title { get; set; }
		public DateTime? DeletedAt { get; set; } = null;

		// Foreign keys and Navigation properties
		public CategoryType CategoryId { get; set; }
		public Category Category { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public ICollection<DailyLogToDoItem> DailyLogToDoItems { get; set; }
	}

}
