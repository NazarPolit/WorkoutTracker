using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
	public class CreateAndUpdateExerciseTypeDto
	{
		public required string Name { get; set; }
		public string? Description { get; set; }
		public string? MuscleGroup { get; set; }
	}
}
