using BusinessLogic.Dtos;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
	public interface ISetService
	{
		Task<Set> AddSetAsync(CreateSetDto dto, string userId);
		Task<string?> UpdateSetAsync (int setId, UpdateSetDto dto, string userId);
		Task<string?> DeleteSetAsync (int setId, string userId);
	}
}
