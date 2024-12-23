using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos.Chatbot;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.Chatbot;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class ChatLogService: BaseService<ChatLogDto, ChatLog>, IChatLogService
    {
        private readonly IChatLogRepository _chatLogRepository;

        public ChatLogService(IChatLogRepository chatLogRepository, IMapper mapper) : base(mapper)
        {
            _chatLogRepository = chatLogRepository;
        }

        public Result<ChatLogDto> Create(long userId)
        {
            var chatLog = _chatLogRepository.Create(userId);
            return MapToDto(chatLog);
        }
        public Result<ChatLogDto> Update(ChatLogDto chatLogDto)
        {
            try
            {
                _chatLogRepository.Update(MapToDomain(chatLogDto));
                return chatLogDto;
            }catch (Exception ex)
            {
               return Result.Fail(FailureCode.Internal).WithError(ex.Message);
            }
        }

        public Result<ChatLogDto> GetByUser(long userId)
        {
            var chatLog = _chatLogRepository.GetByUser(userId);
            return MapToDto(chatLog);
        }
    }
}
