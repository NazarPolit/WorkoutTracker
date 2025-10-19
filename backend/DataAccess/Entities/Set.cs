using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{ 
	public class Set
	{
		public int Id { get; set; }
		public int Repetitions { get; set; }
		public decimal Weight { get; set; }
		public int WorkoutExerciseId { get; set; }
		public WorkoutExercise WorkoutExercise { get; set; }
	}
}
