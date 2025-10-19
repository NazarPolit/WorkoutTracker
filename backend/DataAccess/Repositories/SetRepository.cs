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
	public class SetRepository : GenericRepository<Set>, ISetRepository
	{
		public SetRepository(DataContext context) : base(context)
		{
		}

        public async Task<Set?> GetByIdWithWorkout(int id)
        {
            return await _context.Sets
                   .Include(we => we.WorkoutExercise)
                        .ThenInclude(we => we.Workout)
                    .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
