using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TransportDurationDto
    {
        public TimeSpan Time { get; set; }
        public TransportType TransportType { get; set; }
    }
    public enum TransportType
    {
        Car,
        Bicycle,
        Walking
    }
}
