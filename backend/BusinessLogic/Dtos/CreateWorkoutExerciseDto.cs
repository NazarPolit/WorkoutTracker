using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
	public class CreateWorkoutExerciseDto
	{
		public int WorkoutId { get; set; }
		public int ExerciseTypeId { get; set; }
		public string? Note { get; set; }
	}
}
