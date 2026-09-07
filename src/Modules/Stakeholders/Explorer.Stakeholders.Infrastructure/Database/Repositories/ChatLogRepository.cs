using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.Chatbot;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ChatLogRepository: IChatLogRepository
    {
        private readonly StakeholdersContext _stakeholdersContext;
        private readonly DbSet<ChatLog> _chatLogs;
        
        public ChatLogRepository(StakeholdersContext stakeholdersContext)
        {
            _stakeholdersContext = stakeholdersContext;
            _chatLogs =_stakeholdersContext.Set<ChatLog>();
        }

        public ChatLog Create(long userId) 
        {
            var chatLog = new ChatLog(userId);
            _chatLogs.Add(chatLog);
            _stakeholdersContext.SaveChanges();
            return chatLog;
        }

        public ChatLog GetByUser(long userId) 
        {
            var chatLog = _chatLogs.Where(cl => cl.UserId == userId).OrderByDescending(cl => cl.CreatedAt).FirstOrDefault();
            return chatLog == null ? Create(userId) : chatLog;
        }


        public ChatLog GetById(long id)
        {
            var chatLog = _chatLogs.Where(cl => cl.Id == id).FirstOrDefault();
            return chatLog == null ? throw new KeyNotFoundException("Invalid Chat Log Id") : chatLog;
        }

        public ChatLog Update(ChatLog chatLog) 
        {
            _stakeholdersContext.Update(chatLog);
            _stakeholdersContext.SaveChanges();
            return chatLog;
        }
    }
}
