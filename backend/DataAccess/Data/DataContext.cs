using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccess.Data
{
    public class DataContext : IdentityDbContext<User>  
    {
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
		public DbSet<ExerciseType> ExerciseTypes { get; set; }
		public DbSet<Workout> Workouts { get; set; }
		public DbSet<Set> Sets { get; set; }
		public DbSet<WorkoutTemplate> WorkoutTemplates { get; set; }
		public DbSet<WorkoutTemplateExercise> WorkoutTemplatesExercises { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<WorkoutTemplateExercise>()
				.HasKey(wte => new { wte.WorkoutTemplateId, wte.ExerciseTypeId });

			// Правило 1: При видаленні тренування (Workout) видалити всі пов'язані з ним вправи (WorkoutExercises)
			modelBuilder.Entity<Workout>()
				.HasMany(w => w.WorkoutExercises)
				.WithOne(we => we.Workout)
				.OnDelete(DeleteBehavior.Cascade);

			// Правило 2: При видаленні вправи (WorkoutExercise) видалити всі пов'язані з нею сети (Sets)
			modelBuilder.Entity<WorkoutExercise>()
				.HasMany(we => we.Sets)
				.WithOne(s => s.WorkoutExercise)
				.OnDelete(DeleteBehavior.Cascade);

		}

	}
}
