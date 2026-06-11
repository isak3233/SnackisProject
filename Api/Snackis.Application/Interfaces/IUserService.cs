using Microsoft.AspNetCore.Http;
using Snackis.Application.ApiDto;
using Snackis.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Interfaces
{
    public interface IUserService
    {
        string? CheckFile(IFormFile file);
        Task<GetAvatarUrlDto> GetAvatarUrlAsync(string userId);
        Task<GetUserDto?> GetUserByEmail(string email);
        Task UpdateAvatarAsync(string userId, IFormFile file);
    }
}
