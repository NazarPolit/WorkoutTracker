using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
	public class WorkoutTemplateExercise
	{
		public int WorkoutTemplateId { get; set; }
		public WorkoutTemplate WorkoutTemplate { get; set; }

		public int ExerciseTypeId { get; set; }
		public required ExerciseType ExerciseType { get; set; }
	}
}
