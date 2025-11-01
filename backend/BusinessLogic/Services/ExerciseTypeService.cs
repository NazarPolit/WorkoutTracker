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
	public class ExerciseTypeService : IExerciseTypeService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ExerciseTypeService(IUnitOfWork unitOfWork, IMapper mapper) 
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<ExerciseType> CreateAsync(CreateAndUpdateExerciseTypeDto dto)
		{
			var exerciseType = _mapper.Map<ExerciseType>(dto);

			await _unitOfWork.ExerciseTypes.AddAsync(exerciseType);
			await _unitOfWork.CompleteAsync();

			return exerciseType;
		}

		public async Task<string?> DeleteAsync(int id)
		{
			var exerciseType = await _unitOfWork.ExerciseTypes.GetByIdAsync(id);

			if (exerciseType == null)
				return "Type of exercise not found.";

			var isInUse = await _unitOfWork.ExerciseTypes.IsInUseAsync(id);

            if (isInUse)
				return "This exercise type cannot be deleted because it is already used in training.";

            _unitOfWork.ExerciseTypes.Delete(exerciseType);

			await _unitOfWork.CompleteAsync();

			return null;
		}

		public async Task<PagedList<ExerciseTypeDto>> GetAllAsync(PaginationParams paginationParams, string? muscleGroup, string? nameSearch)
		{
			var exerciseTypes = await _unitOfWork.ExerciseTypes.GetAllExercisesAsync(paginationParams, muscleGroup, nameSearch);
            var exerciseTypesDto = _mapper.Map<ICollection<ExerciseTypeDto>>(exerciseTypes);

			return new PagedList<ExerciseTypeDto>(exerciseTypesDto, exerciseTypes.TotalCout, exerciseTypes.CurrentPage, exerciseTypes.PageSize);
		}

		public async Task<ExerciseTypeDto> GetByIdAsync(int id)
		{
			var exerciseType = await _unitOfWork.ExerciseTypes.GetByIdAsync(id);

			if (exerciseType == null)
				return null;

			return _mapper.Map<ExerciseTypeDto>(exerciseType);
		}

		public async Task<bool> UpdateAsync(int id, CreateAndUpdateExerciseTypeDto dto)
		{
			var exerciseType = await _unitOfWork.ExerciseTypes.GetByIdAsync(id);

			if(exerciseType == null)
				return false;

			_mapper.Map(dto,exerciseType);

			_unitOfWork.ExerciseTypes.Update(exerciseType);

			await _unitOfWork.CompleteAsync();

			return true;
		}
	}
}
