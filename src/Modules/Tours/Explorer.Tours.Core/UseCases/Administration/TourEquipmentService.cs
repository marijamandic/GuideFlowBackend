﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourEquipmentService : CrudService<TourEquipmentDto, TourEquipment>, ITourEquipmentService
    {
        public TourEquipmentService(ICrudRepository<TourEquipment> repository, IMapper mapper) : base(repository, mapper) { }
        //public TourEquipmentService(ITourEquipmentRepository repository, IMapper mapper) : base(repository, mapper) { }

    }
}
