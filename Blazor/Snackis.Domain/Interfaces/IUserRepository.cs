using Microsoft.AspNetCore.Components.Forms;
using Snackis.Application.ApiDto;
using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.BlazorInterfaces
{
    public interface IUserRepository
    {
        Task<GetAvatarUrlDto?> GetUserAvatarAsync(string userId);
        Task<UserDto?> GetUserFromEmailAsync(string email);
        Task UploadAvatarAsync(MultipartFormDataContent? file, string userId);
    }
}
