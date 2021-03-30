using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Backend.Models
{
    public class Order
    {
        public int Id {get; set;}

        public string UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public bool status { get; set; }

        public virtual User User { get; set; }
    }
}
