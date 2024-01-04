using Microsoft.EntityFrameworkCore;
using System;

namespace DailyRhythms.Models
{
	public class DailyRhythmsContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ToDoItem> ToDoItems { get; set; }
		public DbSet<DailyLog> DailyLogs { get; set; }
		public DbSet<DailyLogToDoItem> DailyLogToDoItems { get; set; }

		public DailyRhythmsContext(DbContextOptions<DailyRhythmsContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// Configure the composite key for DailyLogTask
			modelBuilder.Entity<DailyLogToDoItem>()
	       .HasKey(dlt => new { dlt.DailyLogId, dlt.ToDoItemId });

            foreach (var fk in modelBuilder.Model.GetEntityTypes()
				.Where(e => e.Name== "DailyLogToDoItem")
				.SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<DailyLogToDoItem>()
				.HasOne(dlt => dlt.DailyLog)
				.WithMany(dl => dl.DailyLogToDoItems)
				.HasForeignKey(dlt => dlt.DailyLogId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<DailyLogToDoItem>()
				.HasOne(dlt => dlt.ToDoItem)
				.WithMany(ti => ti.DailyLogToDoItems)
				.HasForeignKey(dlt => dlt.ToDoItemId)
			    .OnDelete(DeleteBehavior.Restrict);

		
			// If you want to use the enum as the primary key for Category
			modelBuilder.Entity<Category>()
				.Property(c => c.Id)
				.HasConversion<int>();
			modelBuilder.Entity<Category>().HasData(
			  new Category { Id = CategoryType.Morning, Name = "Morning" },
			  new Category { Id = CategoryType.Afternoon, Name = "Afternoon" },
			  new Category { Id = CategoryType.Evening, Name = "Evening" },
			  new Category { Id = CategoryType.Anytime, Name = "Anytime" }
	  );

			// Other configurations...

		}
	}
}
