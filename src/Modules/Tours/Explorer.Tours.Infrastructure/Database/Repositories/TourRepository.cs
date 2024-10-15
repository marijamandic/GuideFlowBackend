﻿using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class TourRepository : ITourRepository
    {
        private readonly ToursContext _context;

        public TourRepository(ToursContext context)
        {
            _context = context;
        }

        public Tour Get(int id)
        {
            return _context.Tours.Find(id);
        }

        public List<Tour> GetByStatus(string status)
        {
            return _context.Tours.Where(t => t.Status == status).ToList();
        }
    }
}
