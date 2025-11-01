using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Helper;
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
	public class WorkoutService : IWorkoutService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public WorkoutService(IUnitOfWork unitOfWork, IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
        public async Task<WorkoutDto> CreateWorkoutAsync(CreateAndUpdateWorkoutDto dto, string userId)
		{
            var workout = _mapper.Map<Workout>(dto);
			workout.UserId = userId;

			await _unitOfWork.Workouts.AddAsync(workout);
			await _unitOfWork.CompleteAsync();

			return _mapper.Map<WorkoutDto> (workout);
		}

		public async Task<string?> DeleteWorkoutAsync(int id, string userId)
		{
			var workout = await _unitOfWork.Workouts.GetByIdAsync(id);

			if (workout == null)
				return "Workout not found.";

            if (workout.UserId != userId)
                return "You have no rights to delete this workout.";

            _unitOfWork.Workouts.Delete(workout);
			await _unitOfWork.CompleteAsync();

			return null;
		}

		public async Task<WorkoutDetailDto?> GetWorkoutByIdAsync(int id, string userId)
		{
			var workout = await _unitOfWork.Workouts.GetByIdWithDetailsAsync(id);

			if (workout == null)
				return null;

            if (workout.UserId != userId)
            {
                return null;
            }

            return _mapper.Map<WorkoutDetailDto>(workout);
		}

		public async Task<PagedList<WorkoutDto>> GetWorkoutsByUserIdAsync(
			string userId, 
			PaginationParams paginationParams,
			DateTime? startDate,
			DateTime? endDate)
		{
			var workouts = await _unitOfWork.Workouts.GetWorkoutsByUserIdAsync(userId, paginationParams, startDate, endDate);
			var workoutDto = _mapper.Map<ICollection<WorkoutDto>>(workouts);

			return new PagedList<WorkoutDto>(workoutDto, workouts.TotalCout, workouts.CurrentPage, workouts.PageSize);
		}

		public async Task<string?> UpdateWorkoutAsync(int id, CreateAndUpdateWorkoutDto dto, string userId)
		{
			var workout = await _unitOfWork.Workouts.GetByIdAsync(id);

			if (workout == null)
				return "Workout not found.";

            if (workout.UserId != userId)
                return "You have no rights to edit this workout.";

            _mapper.Map(dto, workout);

            _unitOfWork.Workouts.Update(workout);
            await _unitOfWork.CompleteAsync();

			return null;
		}
	}
}
