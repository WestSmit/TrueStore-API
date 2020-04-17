using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {        
        User CreateUser(User user, string password); // return UserId       
        User ValidateUser(string userName, string password);
        bool HasValidRefreshToken(string userId);
        void SetRefreshToken(User user, string token);
        string GetRefreshToken(string UserId);
        User GetUser(string userId);
        string GetRole(User user);
        User Update(User user);
        User ChangeEmail(string userId, string newEmail, string token);
        void ChangePassword(string userId, string currentPassword, string newPassword);
        string GenereteEmailConfirmToken(User user);
        void ConfirmEmail(string userId, string token);
        string GenereteEmailChangingToken(string userId, string newEmail);

    }
}
