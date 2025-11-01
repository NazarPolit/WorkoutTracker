using AutoMapper;
using BusinessLogic.Dtos;
using BusinessLogic.Helper;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
	public class WorkoutTemplateService : IWorkoutTemplateService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public WorkoutTemplateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<PagedList<WorkoutTemplateDto>> GetAllTemplatesAsync(PaginationParams paginationParams, string? nameSearch)
		{
			var templates = await _unitOfWork.WorkoutTemplates.GetAllTemplatesAsync(paginationParams, nameSearch);
			var workoutTemplatesDto = _mapper.Map <ICollection<WorkoutTemplateDto>>(templates);

			return new PagedList<WorkoutTemplateDto>(workoutTemplatesDto, templates.TotalCout, templates.CurrentPage, templates.PageSize);
		}

		public async Task<WorkoutTemplateDetailDto?> GetTemplateByIdAsync(int id)
		{
			var template = await _unitOfWork.WorkoutTemplates.GetByIdWithExercisesAsync(id);

			if (template == null)
				return null;

			return _mapper.Map<WorkoutTemplateDetailDto>(template);
		}


	}
}
