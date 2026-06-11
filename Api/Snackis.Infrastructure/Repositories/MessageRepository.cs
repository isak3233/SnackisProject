using Microsoft.EntityFrameworkCore;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly SnackisDbContext _dbContext;
        public MessageRepository(SnackisDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<SnackisMessage>> GetMessagesAsync(string userId1, string userId2, int start, int count)
        {
            var messages = await _dbContext.SnackisMessages
                .Include(x => x.SenderUser)
                .Include(x => x.ReceiverUser)
                .Where(x => (x.SenderUserId == userId1 || x.SenderUserId == userId2) && (x.ReceiverUserId == userId1 || x.ReceiverUserId == userId2))
                .OrderBy(x => x.SentAt) 
                .Skip(start)
                .Take(count)
                .ToListAsync();
            return messages;
        }
        public async Task<List<SnackisMessage>> GetLatestContactsAsync(string userId, int start, int count)
        {
            // Tar ut alla meddelanden du har skickat och tagit emot. Grupperare dom enligt den andra personen du har skrivit med. tar ut första meddelandet från varje grupp. 

            var latestContacts = await _dbContext.SnackisMessages
                .Include(x => x.SenderUser)
                .Include(x => x.ReceiverUser)
                .Where(x => x.ReceiverUserId == userId || x.SenderUserId == userId)
                .GroupBy(x => x.SenderUserId == userId ? x.ReceiverUserId : x.SenderUserId)
                .Select(g => g
                    .OrderByDescending(x => x.SentAt)
                    .First())
                .Skip(start)
                .Take(count)
                .ToListAsync();

            return latestContacts;
        }
        public async Task CreateMessageAsync(SnackisMessage message)
        {
            _dbContext.SnackisMessages.Add(message);
            await _dbContext.SaveChangesAsync();
        }
    }
}
