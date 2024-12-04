﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }    
        public UserRole Role { get; set; }
        public LocationDto Location { get; set; }
        public bool? IsActive { get; set; } = true;
    }

}
