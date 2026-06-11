using Snackis.Application.BlazorInterfaces;
using Snackis.Domain.BlazorInterfaces;
using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.BlazorServices
{
    public class MessageServiceB : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageServiceB(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }
        public async Task<List<MessageDto>?> GetMessagesAsync(string userId, string receiverUserId, int start, int count)
        {
            var messages = await _messageRepository.GetMessagesAsync(userId, receiverUserId, start, count);
            return messages;
        }
        public async Task<List<ContactDto>?> GetLatestContactsAsync(string userId, int start, int count)
        {
            var contacts = await _messageRepository.GetLatestContactsAsync(userId, start, count);
            return contacts;
        }
        public async Task CreateMessageAsync(string userId, string receiverUserId, string textContent)
        {
            var messageDto = new CreateMessageDto
            {
                ReceiverUserId = receiverUserId,
                TextContent = textContent
            };
            await _messageRepository.CreateMessageAsync(userId, messageDto);
        }
    }
}
