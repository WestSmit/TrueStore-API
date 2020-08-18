using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityResult> CreateUser(User user, string password);
        Task<bool> ValidateUser(User user, string password);
        Task<User> GetUserByName(string username);
        bool HasValidRefreshToken(string userId);
        void SetRefreshToken(User user, string token);
        string GetRefreshToken(string UserId);
        Task<User> GetUser(string userId);
        string GetRole(User user);
        User Update(User user);
        User ChangeEmail(string userId, string newEmail, string token);
        void ChangePassword(string userId, string currentPassword, string newPassword);
        string GenereteEmailConfirmToken(User user);
        void ConfirmEmail(string userId, string token);
        string GenereteEmailChangingToken(string userId, string newEmail);

    }
}
