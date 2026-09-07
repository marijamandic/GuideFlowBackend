using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CheckpointAdditionDto
    {
        public CheckpointDto Checkpoint { get; set; }
        public double UpdatedLength { get; set; }
    }
}
