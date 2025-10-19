using BusinessLogic.Dtos;
using BusinessLogic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
	public interface IWorkoutService 
	{
		Task<PagedList<WorkoutDto>> GetWorkoutsByUserIdAsync(
			string userId, 
			PaginationParams paginationParams,
			DateTime? startDate,
			DateTime? endDate);
		Task<WorkoutDto> CreateWorkoutAsync(CreateAndUpdateWorkoutDto dto, string userId);
		Task<WorkoutDetailDto?> GetWorkoutByIdAsync (int id, string userId);
		Task<string?> UpdateWorkoutAsync(int id, CreateAndUpdateWorkoutDto dto, string userId);
		Task<string?> DeleteWorkoutAsync(int id, string userId);
	}
}
