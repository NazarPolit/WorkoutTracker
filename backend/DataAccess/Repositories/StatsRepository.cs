using DataAccess.Data;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly DataContext _context;

        public StatsRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<decimal?> GetMaxWeightForExerciseAsync(string userId, int exerciseTypeId)
        {
            var maxWeight = await _context.Sets
                .Include(s => s.WorkoutExercise)
                    .ThenInclude(we => we.Workout)
                .Where(s => s.WorkoutExercise.ExerciseTypeId == exerciseTypeId &&
                            s.WorkoutExercise.Workout.UserId == userId)
                .Select(s => s.Weight)
                .DefaultIfEmpty()
                .MaxAsync();

            return maxWeight == 0 ? null : maxWeight;
        }
    }
}
