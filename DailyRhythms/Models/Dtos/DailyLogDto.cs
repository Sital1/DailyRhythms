namespace DailyRhythms.Models.Dtos
{


	#region DailyLog Dtos
	public class DailyLogDto
	{
		public int Id { get; set; }
		public DateOnly Date { get; set; }
		public CategoryToDoItemDto Morning { get; set; }
		public CategoryToDoItemDto Afternoon { get; set; }
		public CategoryToDoItemDto Evening { get; set; }
		public CategoryToDoItemDto Anytime { get; set; }

	}


	public class CategoryToDoItemDto
	{
		public List<ToDoItemDto> ToDoItems { get; set; }
	}
	public class ToDoItemDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public bool IsCompleted { get; set; }
	}

	public class DailyLogToDoItemDto
	{
		public int DailyLogId { get; set; }
		public int ToDoItemId { get; set; }
	}

	#endregion

	#region These are DTO for user tracked ToDoItems. Got nth to do with daily logs
	public class UserToDoItems
	{
		public int Id { get; set; }
		public UserCategoryDto Morning { get; set; }
		public UserCategoryDto Afternoon { get; set; }
		public UserCategoryDto Evening { get; set; }
		public UserCategoryDto Anytime { get; set; }
	}

	public class UserCategoryDto
	{
		public List<UserToDoItemDto> UserToDoItems { get; set; }
	}

	public class UserToDoItemDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime CreatedAt { get; set; }
		public int CategoryId { get; set; }
		public int UserId{get;set;}
	}
	#endregion


	public class AddToDoItemToCategoryDto
	{
		public int CategoryId { get; set; }
		public int UserId { get; set; }
		public string Title { get; set; }
	}

	public class DeleteToDoItemFromCategoryDto
	{
		public int CategoryId { get; set; }
		public int UserId { get; set; }
		public int Id { get; set; }
	}
}
