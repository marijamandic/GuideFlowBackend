using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class UserLocationDto
    {
        public UserDto User { get; set; }
        public LocationDto Location { get; set; }
    }
}
