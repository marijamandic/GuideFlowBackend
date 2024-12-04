using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Explorer.Tours.Core.Domain.TourExecutions;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourSpecificationRepository : ITourSpecificationRepository
    {
        private readonly ToursContext _context;

        public TourSpecificationRepository(ToursContext context)
        {
            _context = context;
        }

        public TourSpecification Create(TourSpecification tourSpecification)
        {
            _context.TourSpecifications.Add(tourSpecification);
            _context.SaveChanges();
            return tourSpecification;
        }

        public TourSpecification Update(TourSpecification tourSpecification)
        {
            if (!_context.TourSpecifications.Any(ts => ts.Id == tourSpecification.Id))
                throw new Exception("Tour specification not found");

            _context.TourSpecifications.Update(tourSpecification);
            _context.SaveChanges();
            return tourSpecification;
        }

        public void Delete(long tourSpecificationId)
        {
            var tourSpecification = _context.TourSpecifications.Find(tourSpecificationId);
            if (tourSpecification == null)
                throw new Exception("Tour specification not found");
             
            _context.TourSpecifications.Remove(tourSpecification);
            _context.SaveChanges();
        }

        public TourSpecification Get(long tourSpecificationId)
        {
            var specification = _context.TourSpecifications
                .Include(t => t.TransportRatings)
                .Where(t => t.Id == tourSpecificationId)
                .FirstOrDefault();
            return specification != null ? specification : throw new Exception("Tour specification not found");
        }

        public PagedResult<TourSpecification> GetPaged(int page, int pageSize)
        {
            var task = _context.TourSpecifications.Include(t => t.TransportRatings).GetPagedById(page, pageSize);
            task.Wait();
            return task.Result;
        }

        public TourSpecification GetByUserId(long userId)
        {
            return _context.TourSpecifications.Include(t => t.TransportRatings)
                    .FirstOrDefault(t => t.UserId == userId);
        }


        public void AddTransportRatings(long tourSpecificationId, IEnumerable<TransportRating> transportRatings)
        {
            var tourSpecification = _context.TourSpecifications
                .Include(t => t.TransportRatings)
                .Where(t => t.Id == tourSpecificationId)
                .FirstOrDefault();

            if (tourSpecification == null)
                throw new Exception("Tour specification not found");

            foreach (var rating in transportRatings)
            {
                rating.Validate();
                tourSpecification.TransportRatings.Add(rating);
            }

            _context.SaveChanges();
        }

    }
}
