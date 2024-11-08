using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using Explorer.Tours.API.Public.Administration;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class PublicPointNotificationService : CrudService<PublicPointNotificationDto, PublicPointNotification>, IPublicPointNotificationService
    {
        private readonly IPublicPointNotificationRepository _publicPointNotificationRepository;
        private readonly IMapper _mapper;

        public PublicPointNotificationService(ICrudRepository<PublicPointNotification> repository, IMapper mapper, IPublicPointNotificationRepository publicPointNotificationRepository)
            : base(repository, mapper)
        {
            _publicPointNotificationRepository = publicPointNotificationRepository;
            _mapper = mapper;
        }

        public Result<IEnumerable<PublicPointNotificationDto>> GetUnreadByAuthor(int authorId)
        {
            try
            {
                // Retrieve unread notifications for the specific author
                var unreadNotifications = _publicPointNotificationRepository.GetAllByAuthor(authorId)
                    .Where(notification => !notification.IsRead)
                    .ToList();

                // Map the result to DTOs
                var dtoList = _mapper.Map<IEnumerable<PublicPointNotificationDto>>(unreadNotifications);

                // Return success result with mapped data
                return Result.Ok(dtoList);
            }
            catch (Exception ex)
            {
                // In case of an error, return a failure result with the exception
                return Result.Fail(new Error("Failed to retrieve unread notifications by author.").CausedBy(ex));
            }
        }

        // Optional: Add other methods for different operations if needed (e.g. mark as read, get all notifications, etc.)
    }
}
