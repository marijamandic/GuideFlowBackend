using AutoMapper;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class NotificationMoneyExchangeService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public NotificationMoneyExchangeService(IMapper mapper, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        public Result CreateNotification(NotificationDto notificationDto)
        {
            try
            {
                var notification = _mapper.Map<Notification>(notificationDto);
                _notificationRepository.Create(notification);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Failed to create notification: {ex.Message}");
            }
        }

        public Result<IEnumerable<NotificationDto>> GetNotificationsByUserId(int id)
        {
            try
            {
                var notifications = _notificationRepository.GetAll()
                    .Where(n => n.UserId == id);
                var notificationDtos = _mapper.Map<IEnumerable<NotificationDto>>(notifications);
                return Result.Ok(notificationDtos);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Failed to fetch notifications for user {id}: {ex.Message}");
            }
        }

        public Result UpdateNotification(long id, NotificationDto updatedNotification)
        {
            try
            {
                var existingNotification = _notificationRepository.NotificationById(id);
                if (existingNotification == null)
                {
                    return Result.Fail("Notification not found.");
                }

                existingNotification.UpdateIsOpened(updatedNotification.IsOpened);

                _notificationRepository.SaveNotification(existingNotification);

                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Fail($"Failed to update notification: {ex.Message}");
            }
        }

    }
}
