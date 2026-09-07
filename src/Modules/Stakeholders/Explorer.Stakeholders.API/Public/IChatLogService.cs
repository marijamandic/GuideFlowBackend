using Explorer.Stakeholders.API.Dtos.Chatbot;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IChatLogService
    {
        Result<ChatLogDto> Create(long userId);
        Result<ChatLogDto> Update(ChatLogDto chatLogDto);

        Result<ChatLogDto> GetByUser(long userId);

    }
}
