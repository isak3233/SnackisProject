using Azure.Core;
using Snackis.Domain.BlazorInterfaces;
using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Snackis.Infrastructure.BlazorRepositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly HttpClient _http;
        public MessageRepository(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("SnackisApi");
        }
        public async Task<List<MessageDto>?> GetMessagesAsync(string userId, string receiverUserId, int start, int count)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"message/{receiverUserId}?start={start}&count={count}");

            request.Headers.Add("User-Id", userId);

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<MessageDto>>();
        }

        public async Task<List<ContactDto>?> GetLatestContactsAsync(string userId, int start, int count)
        {

            var request = new HttpRequestMessage(HttpMethod.Get, $"message/latestcontacts?start={start}&count={count}");

            request.Headers.Add("User-Id", userId);

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<ContactDto>>();

        }
        public async Task CreateMessageAsync(string userId, CreateMessageDto createMessageDto)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"message");

            request.Headers.Add("User-Id", userId);

            request.Content = JsonContent.Create(createMessageDto);

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
