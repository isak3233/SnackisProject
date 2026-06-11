using Microsoft.AspNetCore.Components.Forms;
using Snackis.Application.ApiDto;
using Snackis.Application.BlazorInterfaces;
using Snackis.Domain.DTO;
using Snackis.Domain.BlazorInterfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Snackis.Application.BlazorServices
{
    public class UserServiceB : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserServiceB(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string?> GetUserAvatarAsync(string userId)
        {
            try
            {
                var avatarUrlDto = await _userRepository.GetUserAvatarAsync(userId);
                return avatarUrlDto.AvatarUrl;
            } catch (Exception ex)
            {
                return "";
            }
        }
        public async Task<ContactDto?> GetContactFromEmailAsync(string email)
        {
            try
            {
                var userDto = await _userRepository.GetUserFromEmailAsync(email);
                var contactDto = new ContactDto
                {
                    UserId = userDto.UserId,
                    DisplayName = userDto.DisplayName,
                };
                return contactDto;
            } catch (Exception ex)
            {
                return null;
            }

            
        }
        public async Task UploadAvatarAsync(IBrowserFile file, string userId)
        {
            var content = new MultipartFormDataContent();

            var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);

            var fileContent = new StreamContent(stream);

            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            content.Add(fileContent, "file", file.Name);

            await _userRepository.UploadAvatarAsync(content, userId);
        }
    }
}
