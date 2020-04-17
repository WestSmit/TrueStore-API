using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace DAL.Entities
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ActualTokenLastDate { get; set; }
    }
}
