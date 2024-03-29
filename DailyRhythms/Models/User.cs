﻿using System.ComponentModel.DataAnnotations;

namespace DailyRhythms.Models
{
	public class User : BaseEntity
	{
		// Inherits 'Id', 'CreatedAt', and 'UpdatedAt' from BaseEntity
		[Required(ErrorMessage ="DisplayName is requied")]
		public string DisplayName { get; set; }
		
		[EmailAddress]
		[Required]
		public string Email { get; set; }

		[Required]
		public string TimeZoneId { get; set; }  // IANA time zone identifier

		[Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*[a-z])(?=.*[^\w\d\s:])([^\s]){8,16}$",
            ErrorMessage = "Password must have 1 Uppercase, 1 Lowercase, 1 number, 1 non alphanumeric and between 8 to 16 characters")]
        public string Password { get; set; }

		// Navigation properties
		public ICollection<ToDoItem> ToDoItems { get; set; }
		public ICollection<DailyLog> DailyLogs { get; set; }
	}
}
