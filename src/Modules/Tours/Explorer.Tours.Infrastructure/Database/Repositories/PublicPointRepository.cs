using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class PublicPointRepository : IPublicPointRepository
    {
        private readonly ToursContext _context;

        public PublicPointRepository(ToursContext context)
        {
            _context = context;
        }
        public IEnumerable<PublicPoint> GetAll()
        {
            return _context.PublicPoints.ToList();
        }
    }
}
