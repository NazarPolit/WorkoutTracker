using BusinessLogic.Dtos;
using BusinessLogic.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
	public interface IWorkoutTemplateService
	{
		Task<PagedList<WorkoutTemplateDto>> GetAllTemplatesAsync(PaginationParams paginationParams, string? nameSearch);
		Task<WorkoutTemplateDetailDto?> GetTemplateByIdAsync(int id);
	}
}
