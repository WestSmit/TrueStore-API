using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities
{
    public class Role : IdentityRole
    {
        public Role() { }
        public Role(string name)
        {
            Name = name;
            NormalizedName = name.ToUpper();
        }
    }
}
