using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<SnackisMessage>> GetMessagesAsync(string userId1, string userId2, int start, int count);
        Task<List<SnackisMessage>> GetLatestContactsAsync(string userId, int start, int count);
        Task CreateMessageAsync(SnackisMessage message);
    }
}
