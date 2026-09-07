using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public enum Status { Added = 0, Removed = 1 }
    public class EquipmentManagementDto
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public int EquipmentId { get; set; }
        public Status Status { get; set; }
        
    }
}
