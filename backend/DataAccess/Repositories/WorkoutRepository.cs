using BusinessLogic.Helper;
using DataAccess.Data;
using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
	public class WorkoutRepository : GenericRepository<Workout>, IWorkoutRepository
	{
        public WorkoutRepository(DataContext context) : base(context)
        {
            
        }

		public async Task<Workout?> GetByIdWithDetailsAsync(int id)
		{
			return await _context.Workouts
						.Include(w => w.WorkoutExercises)
							.ThenInclude(we => we.ExerciseType)
						.Include(w => w.WorkoutExercises)
							.ThenInclude(we => we.Sets)
						.FirstOrDefaultAsync(w => w.Id == id);
		}

		public async Task<PagedList<Workout>> GetWorkoutsByUserIdAsync(
			string userId, 
			PaginationParams paginationParams,
			DateTime? startDate,
			DateTime? endDate)
		{
			var query =  _context.Workouts
				   .Where(w => w.UserId == userId)
				   .AsQueryable();

			if(startDate.HasValue)
			{
				query = query.Where(w => w.Date >= startDate.Value);
			}

			if(endDate.HasValue)
			{
				query = query.Where(w => w.Date <= endDate.Value.Date.AddDays(1).AddTicks(-1));
			}

			query = query.OrderByDescending(w => w.Date);

			return await PagedList<Workout>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
		}
	}
}
