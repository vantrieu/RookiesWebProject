using System;

namespace Web.CustomerSite.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTimeOffset ExpiresAt { get; set; }

        public bool TokenExpired
        {
            get
            {
                return ExpiresAt.ToUniversalTime() <= DateTime.UtcNow.AddSeconds(60);
            }
        }
    }
}
