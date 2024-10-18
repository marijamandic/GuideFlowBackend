using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos;
public class ProblemConstantsDto
{
    public List<string> Categories { get; set; }
    public List<string> Priorities { get; set; }
}
