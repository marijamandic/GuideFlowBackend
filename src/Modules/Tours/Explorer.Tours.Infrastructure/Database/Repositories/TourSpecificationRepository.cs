using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourSpecificationRepository : ITourSpecificationRepository
    {
        private readonly ToursContext _context;

        public TourSpecificationRepository(ToursContext context)
        {
            _context = context;
        }

        public bool Exists(int tourSpecificationId)
        {
            return _context.TourSpecifications.Any(t => t.Id == tourSpecificationId);
        }

        public TourSpecifications? GetById(int tourSpecificationId)
        {
            return _context.TourSpecifications
                           .FirstOrDefault(t => t.Id == tourSpecificationId);
        }

        public IEnumerable<TourSpecifications> GetAll()
        {
            return _context.TourSpecifications.ToList();
            //return _context.Set<TourSpecifications>().ToList();
        }

        public TourSpecifications Create(TourSpecifications tourSpecification)
        {
            _context.TourSpecifications.Add(tourSpecification);
            _context.SaveChanges(); // Save changes to the database
            return tourSpecification;
        }

        public void Update(TourSpecifications tourSpecification)
        {
            _context.TourSpecifications.Update(tourSpecification);
            _context.SaveChanges(); // Save changes to the database
        }

        public void Delete(long tourSpecificationId)
        {
            var tourSpecification = _context.TourSpecifications.Find(tourSpecificationId);
            if (tourSpecification != null)
            {
                _context.TourSpecifications.Remove(tourSpecification);
                _context.SaveChanges(); // Save changes to the database
            }
            else
            {
                throw new ArgumentException("Tour specification not found");
            }
        }
    }
}
