using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
    public class AuthResponseDto
    {
        public required string Token { get; set; }
        public required string Email { get; set; }
    }
}
