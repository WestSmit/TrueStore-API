using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserRepository(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> GetUserByName(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<bool> ValidateUser(User user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            return result.Succeeded;
        }

        public async Task<IdentityResult> CreateUser(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public string GenereteEmailConfirmToken(User user)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
        }

        public void ConfirmEmail(string userId, string token)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            var result = _userManager.ConfirmEmailAsync(user, token.Replace(' ', '+'));
            if (!result.Result.Succeeded)
            {
                var message = new StringBuilder().AppendJoin(';', result.Result.Errors.Select(e => e.Description)).ToString();
                throw new Exception(message);
            }
        }

        public bool HasValidRefreshToken(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;
            if (DateTime.Now < user.ActualTokenLastDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetRefreshToken(string UserId)
        {
            return _userManager.Users.Single(u => u.Id == UserId).RefreshToken;
        }

        public void SetRefreshToken(User user, string token)
        {
            user.ActualTokenLastDate = DateTime.Now.AddDays(1);
            Encoding utf8 = Encoding.UTF8;
            user.RefreshToken = token.Normalize();
            var result = _userManager.UpdateAsync(user);
            if (!result.Result.Succeeded)
            {
                var message = new StringBuilder().AppendJoin(';', result.Result.Errors.Select(e => e.Description)).ToString();
                throw new Exception(message);
            }
        }

        public async Task<User> GetUser(string userId)
        {
            var result = await _userManager.FindByIdAsync(userId);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("User is Null");
            }

        }

        public string GetRole(User user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;
            if (roles != null)
            {
                return roles.Single();
            }
            else
            {
                throw new Exception("role is not received");
            }
        }

        public User Update(User user)
        {
            var _user = _userManager.FindByIdAsync(user.Id).Result;

            _user.FirstName = user.FirstName;
            _user.SecondName = user.SecondName;
            _user.UserName = user.UserName;
            _user.PhoneNumber = user.PhoneNumber;

            var result = _userManager.UpdateAsync(_user);
            if (!result.Result.Succeeded)
            {
                var message = new StringBuilder().AppendJoin(';', result.Result.Errors.Select(e => e.Description)).ToString();
                throw new Exception(message);
            }
            else
            {
                return _user;
            }
        }

        public User ChangeEmail(string userId, string newEmail, string token)
        {
            var _user = _userManager.FindByIdAsync(userId).Result;
            if (_user == null)
            {
                throw new Exception("User not found");
            }

            var result = _userManager.ChangeEmailAsync(_user, newEmail, token);
            if (result.Result.Succeeded)
            {
                return _user;
            }
            else
            {
                var message = new StringBuilder().AppendJoin(';', result.Result.Errors.Select(e => e.Description)).ToString();
                throw new Exception(message);
            }
        }

        public string GenereteEmailChangingToken(string userId, string newEmail)
        {
            var _user = _userManager.FindByIdAsync(userId).Result;
            if (_user == null)
            {
                throw new Exception("User not found");
            }
            return _userManager.GenerateChangeEmailTokenAsync(_user, newEmail).Result;
        }

        public void ChangePassword(string userId, string currentPassword, string newPassword)
        {
            var _user = _userManager.FindByIdAsync(userId).Result;
            if (_user == null)
            {
                throw new Exception("User not found");
            }
            var result = _userManager.ChangePasswordAsync(_user, currentPassword, newPassword);
            if (!result.Result.Succeeded)
            {
                var message = new StringBuilder().AppendJoin(';', result.Result.Errors.Select(e => e.Description)).ToString();
                throw new Exception(message);
            }
        }
    }
}
