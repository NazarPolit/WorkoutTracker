using BusinessLogic.Helper;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
	public interface IExerciseTypeRepository : IGenericRepository<ExerciseType>
	{
        Task<PagedList<ExerciseType>> GetAllExercisesAsync(PaginationParams paginationParams,string? muscleGroup, string? nameSearch);
        Task<bool> IsInUseAsync(int id);
	}
}
