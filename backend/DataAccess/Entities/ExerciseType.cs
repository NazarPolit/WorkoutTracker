using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
	public class ExerciseType
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string MuscleGroup { get; set; }
		public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
	}
}
