using BusinessLogic.Helper;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
	public interface IWorkoutRepository : IGenericRepository<Workout>
	{
		Task<PagedList<Workout>> GetWorkoutsByUserIdAsync(
			string userId, 
			PaginationParams paginationParams, 
			DateTime? startDate,
			DateTime? endDate );
		Task<Workout?> GetByIdWithDetailsAsync(int id);
	}
}
