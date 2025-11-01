using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
	public class WorkoutExerciseService : IWorkoutExerciseService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public WorkoutExerciseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<WorkoutExercise> AddExerciseToWorkoutAsync(CreateWorkoutExerciseDto dto, string userId)
		{
            var workoutExercise = _mapper.Map<WorkoutExercise>(dto);

			await _unitOfWork.WorkoutExercises.AddAsync(workoutExercise);
			await _unitOfWork.CompleteAsync();

			return workoutExercise;
		}

		public async Task<string?> RemoveExerciseFromWorkoutAsync(int workoutExerciseId, string userId)
		{
			var workoutExercise = await _unitOfWork.WorkoutExercises.GetByIdWithWorkout(workoutExerciseId);

			if (workoutExercise == null)
			{
				return "The exercise in the workout not found";
			}

			if (workoutExercise.Workout.UserId != userId)
				return "You have no rights to delete this exercise";

			_unitOfWork.WorkoutExercises.Delete(workoutExercise);
			await _unitOfWork.CompleteAsync();

			return null;

		}
	}
}
