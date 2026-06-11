using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Interfaces
{
    public interface IAuthService
    {
        Task<SnackisUser?> AuthorizeAsync(string apiKey, string userId, params string[] roles);
        Task<SnackisUser?> GetUserAsync(string apiKey, string userId);
        Task<bool> UserIsInRoleAsync(SnackisUser user, string role);
    }
}
