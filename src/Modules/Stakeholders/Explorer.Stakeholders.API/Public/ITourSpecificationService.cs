using Explorer.Stakeholders.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public;

public interface ITourSpecificationService
{
    TourSpecificationDto CreateTourSpecifications(TourSpecificationDto TourSpecificationDto);
    IEnumerable<TourSpecificationDto> GetAllTourSpecifications();
    void UpdateTourSpecifications(TourSpecificationDto TourSpecificationDto);
    void DeleteTourSpecifications(long id);
    TourSpecificationDto GetTourSpecificationsByUserId(long userId);

}
