using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
	public class WorkoutExerciseDto
	{
		public int Id { get; set; }
		public required string ExerciseName { get; set; }
		public string? Note {  get; set; }
		public List<SetDto> Sets { get; set; } = new();
	}
}
