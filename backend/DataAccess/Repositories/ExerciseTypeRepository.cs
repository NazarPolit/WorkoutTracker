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
	public class ExerciseTypeRepository : GenericRepository<ExerciseType>, IExerciseTypeRepository
	{
		public ExerciseTypeRepository(DataContext context) : base(context)
		{

		}

        public async Task<PagedList<ExerciseType>> GetAllExercisesAsync(PaginationParams paginationParams, string? muscleGroup, string? nameSearch)
        {
            var query = _context.ExerciseTypes.AsQueryable();

			if(!string.IsNullOrEmpty(muscleGroup))
			{
				query = query.Where(et => et.MuscleGroup == muscleGroup);
			}

			if (!string.IsNullOrEmpty(nameSearch))
			{
                query = query.Where(et => et.Name.ToLower().Contains(nameSearch.ToLower()));
            }

			return await PagedList<ExerciseType>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<bool> IsInUseAsync(int id)
		{
			return await _context.WorkoutExercises.AnyAsync(we => we.ExerciseTypeId == id);
		}
	}
}
