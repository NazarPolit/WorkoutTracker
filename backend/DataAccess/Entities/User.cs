using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Entities
{
	public class User : IdentityUser
	{
		public ICollection<Workout> Workouts { get; set; }
	}
}
