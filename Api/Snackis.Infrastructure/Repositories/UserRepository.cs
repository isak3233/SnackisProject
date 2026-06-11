using Microsoft.AspNetCore.Identity;
using Snackis.Domain.Entities;
using Snackis.Domain.Interfaces;
using Snackis.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Snackis.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<SnackisUser> _userManager;
        public UserRepository(UserManager<SnackisUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<SnackisUser?> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }
        public async Task<SnackisUser?> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }
        public async Task SaveAvatarUrlAsync(string avatarUrl, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.AvatarUrl = avatarUrl;
            await _userManager.UpdateAsync(user);
        }
        public async Task<string?> GetAvatarUrlAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user.AvatarUrl;
        }
    }
}
