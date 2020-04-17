using System;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Helpers
{
    static class TokenGenerator
    {
        public static string Generate(int size = 32)
        {
            var randomNumber = new byte[size];
            using (var rng = RandomNumberGenerator.Create())
            {
                
                rng.GetBytes(randomNumber);
                var str =  Convert.ToBase64String(randomNumber);

                StringBuilder sb = new StringBuilder();
                foreach (char c in str)
                {
                    if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                    {
                        sb.Append(c);
                    }
                }
                return sb.ToString();
            }
        }
    }
}
