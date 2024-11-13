using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Database.Repositories
{
    public class PublicPointNotificationRepository : IPublicPointNotificationRepository
    {
        private readonly ToursContext _context;

        public PublicPointNotificationRepository(ToursContext context)
        {
            _context = context;
        }

        public IEnumerable<PublicPointNotification> GetAllByAuthor(int authorId)
        {
            return _context.PublicPointNotifications
                           .Where(notification => notification.AuthorId == authorId)
                           .ToList();
        }
    }
}
