using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Models
{
    public class PasswordModel
    {
        public string UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        
    }
}
