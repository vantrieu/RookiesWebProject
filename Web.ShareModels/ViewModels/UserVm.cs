using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ShareModels.ViewModels
{
    public class UserVm
    {
        public string UserId { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }
    }
}
