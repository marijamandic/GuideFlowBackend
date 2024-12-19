using Explorer.Stakeholders.API.Dtos.Explorer.Stakeholders.Core.DTO;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IMessageNotificationService
    {
        Result<MessageNotificationDto> Create(MessageNotificationDto messageNotificationDto);
        Result<MessageNotificationDto> Update(int id,bool isOpened);
        Result<List<MessageNotificationDto>> GetAllByUserId(int userId);
        Result DeleteMessageNotification(int id);
    }
}
