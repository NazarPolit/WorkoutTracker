using DataAccess.Data;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public async Task<List<VolumeHistoryData>> GetVolumeHistoryAsync(string userId, int exerciseTypeId)
        {
            var history = await _context.Sets
                .Include(s => s.WorkoutExercise)
                    .ThenInclude(we => we.Workout)
                .Where(s => s.WorkoutExercise.ExerciseTypeId == exerciseTypeId &&
                            s.WorkoutExercise.Workout.UserId == userId)
                .GroupBy(s => s.WorkoutExercise.Workout.Date.Date)
                .Select(group => new VolumeHistoryData
                {
                    Date = group.Key,
                    TotalVolume = group.Sum(s => s.Weight * s.Repetitions)
                })
                .OrderBy(h => h.Date)
                .ToListAsync();

            return history;
        }
    }
}
