using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
    public class PersonalRecordDto
    {
        public int ExerciseTypeId { get; set; }
        public string ExersiceName { get; set; } = string.Empty;
        public decimal? MaxWeigth { get; set; }
    }
}
