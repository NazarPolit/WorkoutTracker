using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
	public class CreateSetDto
	{
		public int Repetitions { get; set; }
		public decimal Weight { get; set; }
		public int WorkoutExerciseId { get; set; }
	}
}
