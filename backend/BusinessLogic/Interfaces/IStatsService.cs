using BusinessLogic.Dtos;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IStatsService
    {
        Task<PersonalRecordDto> GetUserPersonalRecordAsync(string userId, int exerciseTypeId);
        Task<List<VolumeHistoryData>> GetUserVolumeHistoryAsync(string userId, int exerciseTypeId);
    }
}
