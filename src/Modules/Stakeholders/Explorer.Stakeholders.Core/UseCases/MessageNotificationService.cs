using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Explorer.Stakeholders.Core.DTO;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class MessageNotificationService : BaseService<MessageNotificationDto,MessageNotification> ,IMessageNotificationService
    {
        private readonly IMessageNotificationRepository _messageNotificationRepository;

        public MessageNotificationService(IMessageNotificationRepository messageNotificationRepository , IMapper mapper) : base(mapper)
        {
            _messageNotificationRepository = messageNotificationRepository;
        }

        public Result<MessageNotificationDto> Create(MessageNotificationDto messageNotificationDto)
        {
            var message = _messageNotificationRepository.Create(MapToDomain(messageNotificationDto));
            return MapToDto(message);
        }

        public Result<List<MessageNotificationDto>> GetAllByUserId(int userId)
        {
            var messages = _messageNotificationRepository.GetAllByUserId(userId);
            return MapToDto(messages);
        }

        public Result<MessageNotificationDto> Update(int id,bool isOpened)
        {
            var message = _messageNotificationRepository.GetById(id);
            if (message == null) return Result.Fail("Message not found");
            message.SetIsOpened(isOpened);
            _messageNotificationRepository.Update(message);
            return MapToDto(message);
        }
    }
}
