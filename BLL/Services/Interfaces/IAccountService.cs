using BLL.Models;

namespace BLL.Services.Interfaces
{
    public interface IAccountService
    {

        LoginResultModel UpdateUserInfo(UserModel userModel);
        LoginResultModel ChangeEmail(string userId, string newEmail, string token);
        void ChangePassword(string userId, string currentPassword, string newPassword);
        void TryEmailChanging(string userId, string newEmail);
    }
}
