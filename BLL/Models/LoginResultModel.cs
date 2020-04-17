using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class LoginResultModel : BaseModel
    {
        public UserModel User { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        
    }
}
