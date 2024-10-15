﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public
{
    public interface IEquipmentManagementService
    {
        public List<EquipmentManagementDto> GetEquipmentList();
        List<EquipmentManagementDto> GetEquipmentListForTourist(int touristId);
        EquipmentManagementDto AddEquipment(EquipmentManagementDto equipment);
        EquipmentManagementDto RemoveEquipment(EquipmentManagementDto equipment);


    }
}
