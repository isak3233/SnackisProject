using Snackis.Application.DTO;
using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }
        public async Task<List<MessageDto>> GetMessagesAsync(string userId1, string userId2, int start, int count)
        {
            if (userId1 == userId2) throw new Exception("Can't get messages from same user");
            var messages = await _messageRepository.GetMessagesAsync(userId1, userId2, start, count);
            var messagesDto = messages.Select(x => new MessageDto
            {
                SenderId = x.SenderUserId,
                DisplayName = x.SenderUser.DisplayName,
                AvatarUrl = x.SenderUser.AvatarUrl,
                TextContent = x.TextContent,
                SentAt = x.SentAt,

            }).ToList();
            return messagesDto;
        }
        public async Task<List<ContactDto>> GetLatestContactsAsync(string userId, int start, int count)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) throw new Exception("User not found");
            var latestContactsMessage = await _messageRepository.GetLatestContactsAsync(userId, start, count);
            var orderlist = latestContactsMessage.OrderByDescending(x => x.SentAt);
            var latestContactsDto = new List<ContactDto>();
            foreach (var message in orderlist)
            {
                SnackisUser contactUser;
                if(message.SenderUserId == userId)
                {
                    contactUser = message.ReceiverUser;
                } else
                {
                    contactUser = message.SenderUser;
                }
                var contactDto = new ContactDto
                {
                    UserId = contactUser.Id,
                    DisplayName = contactUser.DisplayName,
                    AvatarUrl = contactUser.AvatarUrl,
                    TextContent = message.TextContent,
                    LatestMessageTime = message.SentAt
                };
                latestContactsDto.Add(contactDto);
            }
            return latestContactsDto;

        }
        public async Task CreateMessageAsync(string senderUserId, CreateMessageDto createMessageDto)
        {
            if (senderUserId == createMessageDto.ReceiverUserId) throw new Exception("Sender cannot be the receiver");
            var receiverUser = await _userRepository.GetUserByIdAsync(createMessageDto.ReceiverUserId);
            if (receiverUser == null) throw new Exception("Receiver not found");
            var message = new SnackisMessage
            {
                ReceiverUserId = createMessageDto.ReceiverUserId,
                SenderUserId = senderUserId,
                SentAt = DateTime.UtcNow,
                TextContent = createMessageDto.TextContent,
            };

            await _messageRepository.CreateMessageAsync(message);
        }
    }
}
