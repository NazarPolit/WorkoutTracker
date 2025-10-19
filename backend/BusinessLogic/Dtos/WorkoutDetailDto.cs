using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
	public class WorkoutDetailDto
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public DateTime Date { get; set; }
		public List<WorkoutExerciseDto> Exercises { get; set; } = new();
	}
}
