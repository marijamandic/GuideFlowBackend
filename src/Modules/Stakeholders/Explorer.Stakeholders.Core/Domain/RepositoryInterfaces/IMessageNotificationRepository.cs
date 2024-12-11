using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IMessageNotificationRepository
    {
        MessageNotification Create(MessageNotification message);
        MessageNotification Update(MessageNotification message);
        List<MessageNotification> GetAllByUserId(int userId);
        MessageNotification? GetById(int id);
    }
}
