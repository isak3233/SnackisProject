using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Snackis.Application.Interfaces;
using Snackis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Application.Services
{
    public class AuthService :IAuthService
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<SnackisUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<SnackisUser?> AuthorizeAsync(string apiKey, string userId, params string[] roles)
        {
            SnackisUser? user = await GetUserAsync(apiKey, userId);
            if (user == null)
            {
                return null;
            }
            foreach (string role in roles)
            {
                if (await UserIsInRoleAsync(user, role) == false)
                {
                    return null;
                }
            }


            return user;

        }
        public async Task<SnackisUser?> GetUserAsync(string apiKey, string userId)
        {


            if (string.IsNullOrWhiteSpace(apiKey) || string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }

            var validApiKey = _configuration["ApiKey"];

            if (apiKey != validApiKey)
            {
                return null;
            }

            var user = await _userManager.FindByIdAsync(userId);

            return user;
        }
        public async Task<bool> UserIsInRoleAsync(SnackisUser user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }
    }
}
