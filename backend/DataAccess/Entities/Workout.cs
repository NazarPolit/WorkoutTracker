﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
	public class Workout
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }
		public string UserId { get; set; }
		public User User { get; set; }
		public ICollection<WorkoutExercise> WorkoutExercises { get; set; }
	}
}
