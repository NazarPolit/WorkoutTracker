using BusinessLogic.Helper;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IWorkoutTemplateRepository : IGenericRepository<WorkoutTemplate>
    {
        Task<WorkoutTemplate?> GetByIdWithExercisesAsync(int id);
        Task<PagedList<WorkoutTemplate>> GetAllTemplatesAsync(PaginationParams paginationParams, string? nameSearch);
    }
}
