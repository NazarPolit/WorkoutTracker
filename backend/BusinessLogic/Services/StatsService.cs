using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class StatsService : IStatsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PersonalRecordDto> GetUserPersonalRecordAsync(string userId, int exerciseTypeId)
        {
            var maxWeigth = await _unitOfWork.Stats.GetMaxWeightForExerciseAsync(userId, exerciseTypeId);

            var exerciseType = await _unitOfWork.ExerciseTypes.GetByIdAsync(exerciseTypeId);

            if (exerciseType == null)
            {
                return null;
            }

            return new PersonalRecordDto
            {
                ExerciseTypeId = exerciseTypeId,
                ExersiceName = exerciseType.Name,
                MaxWeigth = maxWeigth
            };
        }

        public async Task<List<VolumeHistoryData>> GetUserVolumeHistoryAsync(string userId, int exerciseTypeId)
        {
            return await _unitOfWork.Stats.GetVolumeHistoryAsync(userId, exerciseTypeId);
        }
    }
}
