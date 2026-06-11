using Microsoft.AspNetCore.Http;
using Snackis.Application.ApiDto;
using Snackis.Application.DTO;
using Snackis.Application.Interfaces;
using Snackis.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public string? CheckFile(IFormFile file)
        {

            var allowedTypes = new[] { "image/jpeg", "image/png", "image/webp" };

            if (file == null || file.Length == 0)  return "No file";
            if (!allowedTypes.Contains(file.ContentType)) return "Invalid image type";
            if (file.Length > 2 * 1024 * 1024) return "Max 2MB allowed";
            return null;

        }
        public async Task<GetUserDto?> GetUserByEmail(string email)
        {

            var user = await _userRepository.GetUserByEmail(email);
            if (user == null) throw new Exception("User not found");
            var userDto = new GetUserDto
            {
                UserId = user.Id,
                DisplayName = user.DisplayName,

            };
            return userDto;

            
        }
        public async Task<GetAvatarUrlDto> GetAvatarUrlAsync(string userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            var avatarUrl = await _userRepository.GetAvatarUrlAsync(userId);
            var avatarUrlDto = new GetAvatarUrlDto
            {
                AvatarUrl = avatarUrl
            };
            return avatarUrlDto;
        }


        public async Task UpdateAvatarAsync(string userId, IFormFile file)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var path = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/avatars",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var newUrl = $"/avatars/{fileName}";

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) throw new Exception("User not found");

            var oldAvatarUrl = await _userRepository.GetAvatarUrlAsync(userId);

            await _userRepository.SaveAvatarUrlAsync(newUrl, userId);

            if (!string.IsNullOrEmpty(oldAvatarUrl))
            {
                var oldPath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    oldAvatarUrl.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString())
                );

                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }
            }
        }
    }
}
