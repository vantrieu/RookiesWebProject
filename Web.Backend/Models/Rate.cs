using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Backend.Models
{
    public class Rate
    {
        public int ProductId { get; set; }

        public string UserId { get; set; }

        public int TotalRate { get; set; }
    }
}
