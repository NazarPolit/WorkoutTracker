using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public class VolumeHistoryData
    {
        public DateTime Date { get; set; }
        public decimal TotalVolume { get; set; }
    }
    public interface IStatsRepository
    {
        Task<decimal?> GetMaxWeightForExerciseAsync(string userId, int exerciseTypeId);
        Task<List<VolumeHistoryData>> GetVolumeHistoryAsync(string userId, int exerciseTypeId);
    }
}
