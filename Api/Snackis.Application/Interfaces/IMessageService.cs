using Snackis.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Interfaces
{
    public interface IMessageService
    {
        Task<List<MessageDto>> GetMessagesAsync(string userId1, string userId2, int start, int count);
        Task<List<ContactDto>> GetLatestContactsAsync(string userId, int start, int count);
        Task CreateMessageAsync(string senderUserId, CreateMessageDto createMessageDto);
    }
}
