using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Web.ShareModels
{
    public class User : IdentityUser
    {
        public User() : base()
        {
        }

        public User(string userName) : base(userName)
        {
        }

        [PersonalData]
        public string FullName { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
