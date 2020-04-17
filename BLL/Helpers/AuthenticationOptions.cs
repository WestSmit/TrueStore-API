using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BLL.Helpers
{
    public class AuthenticationOptions
    {
        public const string ISSUER = "TrueStoreServer";
        public const string AUDIENCE = "TrueStoreClient";
        const string KEY = "VerySecretKeyForTrueStore";
        public const int LIFETIME = 2;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
