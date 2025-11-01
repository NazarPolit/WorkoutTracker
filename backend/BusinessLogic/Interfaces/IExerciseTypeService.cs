using BusinessLogic.Dtos;
using BusinessLogic.Helper;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
	public interface IExerciseTypeService
	{
		Task<PagedList<ExerciseTypeDto>> GetAllAsync(PaginationParams paginationParams, string? muscleGroup, string? nameSearch);
		Task<ExerciseTypeDto> GetByIdAsync(int id);
		Task<bool> UpdateAsync(int id, CreateAndUpdateExerciseTypeDto dto);
		Task<ExerciseType> CreateAsync(CreateAndUpdateExerciseTypeDto dto);
		Task<string?> DeleteAsync(int id);
		
	}
}
