namespace DailyRhythms.Models.Dtos
{	


	
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

    public class  AddToDoItemToCategoryDto
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
