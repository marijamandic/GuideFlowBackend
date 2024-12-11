using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class MessageNotificationRepository : IMessageNotificationRepository
    {
        private readonly StakeholdersContext _stakeholdersContext;
        private readonly DbSet<MessageNotification> _messageNotifications;
        public MessageNotificationRepository(StakeholdersContext stakeholdersContext)
        {
            _stakeholdersContext = stakeholdersContext;
            _messageNotifications = stakeholdersContext.Set<MessageNotification>();
        }

        public MessageNotification Create(MessageNotification message)
        {
            _messageNotifications.Add(message);
            _stakeholdersContext.SaveChanges();
            return message;
        }

        public List<MessageNotification> GetAllByUserId(int userId)
        {
            return _messageNotifications.Where(mn => mn.UserId == userId).ToList();
        }

        public MessageNotification Update(MessageNotification message)
        {
            try
            {
                _stakeholdersContext.Update(message);
                _stakeholdersContext.SaveChanges();
            }
            catch (DbUpdateException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return message;
        }
        public MessageNotification? GetById(int id) {
            return _messageNotifications.FirstOrDefault(mn => mn.Id == id)?? null;
        }
    }
}
