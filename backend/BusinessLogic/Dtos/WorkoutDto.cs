using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
	public class WorkoutDto
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public DateTime Date { get; set; }
	}
}
