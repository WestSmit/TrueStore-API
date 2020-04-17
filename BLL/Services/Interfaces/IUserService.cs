using BLL.Models;

namespace BLL.Services.Interfaces
{
    public interface IUserService
    {
        void UserRegistration(UserModel user);
        LoginResultModel UserAuthentication(string email, string password);
        LoginResultModel UpdateTokens(string userId);
        bool VerifyRefreshToken(string userId, string token);
        void ConfirmUserEmail(string userId, string token);
    }
}
