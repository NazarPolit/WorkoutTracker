using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		IExerciseTypeRepository ExerciseTypes { get; }
		IWorkoutTemplateRepository WorkoutTemplates { get; }
		IWorkoutRepository Workouts { get; }
		IWorkoutExerciseRepository WorkoutExercises { get; }
		ISetRepository Sets { get; }
		IStatsRepository Stats { get; }

		Task<int> CompleteAsync();
	}
}
