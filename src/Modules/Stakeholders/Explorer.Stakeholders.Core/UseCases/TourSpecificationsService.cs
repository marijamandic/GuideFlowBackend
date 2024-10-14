using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using System.Collections.Generic;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TourSpecificationService : ITourSpecificationService
    {
        // Simuliramo bazu podataka sa listom
        private readonly List<TourSpecificationDto> _tourSpecifications;

        public TourSpecificationService()
        {
            // Inicijalna lista
            _tourSpecifications = new List<TourSpecificationDto>();
        }

        // Kreiramo novu turu
        public TourSpecificationDto CreateTourSpecifications(TourSpecificationDto tourSpecificationDto)
        {
            var existingTourSpec = _tourSpecifications
                .FirstOrDefault(t => t.UserId == tourSpecificationDto.UserId);

            if (existingTourSpec != null)
            {
                throw new ArgumentException("Specifikacija ture za ovog korisnika već postoji.");
            }

            // Dodajemo novu specifikaciju ture u listu
            _tourSpecifications.Add(tourSpecificationDto);

            return tourSpecificationDto;
        }

        // Vraća sve ture
        public IEnumerable<TourSpecificationDto> GetAllTourSpecifications()
        {
            return _tourSpecifications;
        }

        // Ažuriramo postojeću turu
        public void UpdateTourSpecifications(TourSpecificationDto tourSpecificationDto)
        {
            var existingTourSpec = _tourSpecifications
                .FirstOrDefault(t => t.UserId == tourSpecificationDto.UserId);

            if (existingTourSpec != null)
            {
                existingTourSpec.TourDifficulty = tourSpecificationDto.TourDifficulty;
                existingTourSpec.TransportRatings = tourSpecificationDto.TransportRatings;
                existingTourSpec.Tags = tourSpecificationDto.Tags;
            }
            else
            {
                throw new ArgumentException("Specifikacija ture nije pronađena.");
            }
        }

        // Brišemo turu po ID-u
        public void DeleteTourSpecifications(long userId)
        {
            var tourSpec = _tourSpecifications.FirstOrDefault(t => t.UserId == userId);

            if (tourSpec != null)
            {
                _tourSpecifications.Remove(tourSpec);
            }
            else
            {
                throw new ArgumentException("Specifikacija ture nije pronađena.");
            }
        }

        // Vraćamo turu prema UserId
        public TourSpecificationDto GetTourSpecificationsByUserId(long userId)
        {
            var tourSpec = _tourSpecifications.FirstOrDefault(t => t.UserId == userId);

            if (tourSpec != null)
            {
                return tourSpec;
            }
            else
            {
                throw new ArgumentException("Specifikacija ture nije pronađena za zadatog korisnika.");
            }
        }
    }
}
