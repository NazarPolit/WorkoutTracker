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
	public class SetService : ISetService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public SetService(IUnitOfWork unitOfWork, IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Set> AddSetAsync(CreateSetDto dto, string userId)
		{
			var set = _mapper.Map<Set>(dto);

			await _unitOfWork.Sets.AddAsync(set);
			await _unitOfWork.CompleteAsync();

			return set;
		}

		public async Task<string?> DeleteSetAsync(int setId, string userId)
		{
			var set = await _unitOfWork.Sets.GetByIdWithWorkout(setId);

			if (set == null)
				return "Set not found";

            if (set.WorkoutExercise.Workout.UserId != userId)
            {
                return "You have no rights to delete this set";
            }

            _unitOfWork.Sets.Delete(set);
			await _unitOfWork.CompleteAsync();

			return null;
		}

		public async Task<string?> UpdateSetAsync(int setId, UpdateSetDto dto, string userId)
		{
			var set = await _unitOfWork.Sets.GetByIdWithWorkout(setId);

			if (set == null)
				return "Set not found";

			if (set.WorkoutExercise.Workout.UserId != userId)
				return "You have no rights to edit this set";

			_mapper.Map(dto, set);

			_unitOfWork.Sets.Update(set);
			await _unitOfWork.CompleteAsync();

			return null;
		}
	}
}
