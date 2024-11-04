using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.Execution
{
    public class CreateTourExecutionDto
    {
        public int TourId {  get; set; }
        public int UserId { get; set; }

    }
}
