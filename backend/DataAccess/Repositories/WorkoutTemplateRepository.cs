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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataAccess.Repositories
{
	public class WorkoutTemplateRepository : GenericRepository<WorkoutTemplate>, IWorkoutTemplateRepository
	{
		public WorkoutTemplateRepository(DataContext context) : base(context){ }

        public async Task<PagedList<WorkoutTemplate>> GetAllTemplatesAsync(PaginationParams paginationParams, string? nameSearch)
        {
            var query = _context.WorkoutTemplates.AsQueryable();

			if(!string.IsNullOrEmpty(nameSearch))
			{
				query = query.Where(wt => wt.Name.ToLower().Contains(nameSearch.ToLower()));
			}

            return await PagedList<WorkoutTemplate>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<WorkoutTemplate?> GetByIdWithExercisesAsync(int id)
		{
			return await _context.WorkoutTemplates
				.Include(wt => wt.TemplateExercises)
				.ThenInclude(wte => wte.ExerciseType)
				.FirstOrDefaultAsync(wt => wt.Id == id);
		}
	}
}
