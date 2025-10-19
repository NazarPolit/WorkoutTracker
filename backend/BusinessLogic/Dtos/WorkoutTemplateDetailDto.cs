using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
	public class WorkoutTemplateDetailDto
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }
		public List<ExerciseTypeDto> Exercises { get; set; } = new List<ExerciseTypeDto>();
	}
}
