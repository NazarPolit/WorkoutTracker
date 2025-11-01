using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly DataContext _context;
		public IExerciseTypeRepository ExerciseTypes { get; private set; }
		public IWorkoutTemplateRepository WorkoutTemplates { get; private set; }
		public IWorkoutRepository Workouts { get; private set; }
		public IWorkoutExerciseRepository WorkoutExercises { get; private set; }
		public ISetRepository Sets { get; private set; }
		public IStatsRepository Stats { get; private set; }

		public UnitOfWork(DataContext context)
        {
			_context = context;
			ExerciseTypes = new ExerciseTypeRepository(_context);
			WorkoutTemplates = new WorkoutTemplateRepository(_context);
			Workouts = new WorkoutRepository(_context);
			WorkoutExercises = new WorkoutExerciseRepository(_context);
			Sets = new SetRepository(_context);
			Stats = new StatsRepository(_context);

		}

		public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
