using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public enum Status { Added, Removed }
    public class EquipmentManagementDto
    {

        public int TouristId { get; set; }
        public int EquipmentId { get; set; }
        public Status Status { get; set; }
        public List<EquipmentManagementDto> EquipmentList { get; set; }



        public EquipmentManagementDto()
        {
            EquipmentList = new List<EquipmentManagementDto>();
        }
        public EquipmentManagementDto(int touristId, List<EquipmentManagementDto> equipmentList)
        {
            TouristId = touristId;
            EquipmentList = equipmentList ?? new List<EquipmentManagementDto>();
        }
        public EquipmentManagementDto(int touristId, int equipmentId, Status status)
        {
            TouristId = touristId;
            EquipmentId = equipmentId;
            Status = status;

        }
    }
}
