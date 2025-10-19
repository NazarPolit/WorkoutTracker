using BusinessLogic.Dtos;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
	public interface IWorkoutExerciseService
	{
		Task<WorkoutExercise> AddExerciseToWorkoutAsync(CreateWorkoutExerciseDto dto, string userId);
		Task<string?> RemoveExerciseFromWorkoutAsync(int workoutExerciseId, string userId);
	}
}
