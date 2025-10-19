using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
	public class WorkoutExercise
	{
		public int Id { get; set; }
		public string Note { get; set; }
		public int WorkoutId { get; set; }
		public Workout Workout { get; set; }
		public int ExerciseTypeId { get; set; }
		public ExerciseType ExerciseType { get; set; }
		public ICollection<Set> Sets { get; set; }
	}
}
