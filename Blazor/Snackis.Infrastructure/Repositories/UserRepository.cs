using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Snackis.Application.ApiDto;
using Snackis.Domain.BlazorInterfaces;
using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Snackis.Infrastructure.BlazorRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _http;
        public UserRepository(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("SnackisApi");
        }
        public async Task<GetAvatarUrlDto?> GetUserAvatarAsync(string userId)
        {
            var avatarUrl = await _http.GetFromJsonAsync<GetAvatarUrlDto>($"user/{userId}/avatar");
            return avatarUrl;
        }
        public async Task<UserDto?> GetUserFromEmailAsync(string email)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "user/useremail");

            request.Content = JsonContent.Create(email);

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<UserDto>();
        }
        public async Task UploadAvatarAsync(MultipartFormDataContent? file, string userId)
        {

            var request = new HttpRequestMessage(HttpMethod.Post, "user/upload-avatar");

            request.Headers.Add("User-Id", userId);

            request.Content = file;

            var response = await _http.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
