using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<SnackisUser?> GetUserByIdAsync(string userId);
        Task<SnackisUser?> GetUserByEmail(string email);
        Task SaveAvatarUrlAsync(string avatarUrl, string userId);
        Task<string?> GetAvatarUrlAsync(string userId);
    }
}
