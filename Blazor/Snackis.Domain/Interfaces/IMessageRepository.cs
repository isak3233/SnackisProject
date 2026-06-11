using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.BlazorInterfaces
{
    public interface IMessageRepository
    {
        Task<List<MessageDto>?> GetMessagesAsync(string userId, string receiverUserId, int start, int count);

        Task<List<ContactDto>?> GetLatestContactsAsync(string userId, int start, int count);
        Task CreateMessageAsync(string userId, CreateMessageDto createMessageDto);
    }
}
