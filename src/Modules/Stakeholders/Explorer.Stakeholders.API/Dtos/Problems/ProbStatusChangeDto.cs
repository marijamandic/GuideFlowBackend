using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos.Problems
{
    public class ProbStatusChangeDto
    {
        public bool IsSolved { get; set; }
        public string TouristMessage { get; set; }
    }
}
