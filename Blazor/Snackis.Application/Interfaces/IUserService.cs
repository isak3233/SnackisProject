using Microsoft.AspNetCore.Components.Forms;
using Snackis.Application.ApiDto;
using Snackis.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.BlazorInterfaces
{
    public interface IUserService
    {
        Task<string?> GetUserAvatarAsync(string userId);
        Task<ContactDto?> GetContactFromEmailAsync(string email);
        Task UploadAvatarAsync(IBrowserFile file, string userId);
    }
}
