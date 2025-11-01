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
	public class WorkoutExerciseRepository : GenericRepository<WorkoutExercise>, IWorkoutExerciseRepository
	{
		public WorkoutExerciseRepository(DataContext context) : base(context)
		{
		}

        public async Task<WorkoutExercise?> GetByIdWithWorkout(int id)
        {
            return await _context.WorkoutExercises
                         .Include(we => we.Workout)
                        .FirstOrDefaultAsync(we => we.Id == id);
        }
    }
}
