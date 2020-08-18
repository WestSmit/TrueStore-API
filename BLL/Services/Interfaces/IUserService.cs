using BLL.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<IdentityResult> UserRegistration(UserModel user);
        Task<bool> UserAuthentication(string email, string password);
        Task<LoginResultModel> UpdateTokens(string userId);
        Task<LoginResultModel> UpdateTokensByUsername(string username);
        bool VerifyRefreshToken(string userId, string token);
        void ConfirmUserEmail(string userId, string token);
    }
}
