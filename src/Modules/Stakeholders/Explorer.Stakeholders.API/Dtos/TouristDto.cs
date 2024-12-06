using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class TouristDto : UserDto
    {
        public double Wallet { get; set; }
        public int Xp { get; set; }
        public int Level { get; set; }

    }
}
