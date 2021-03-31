using System;
using System.Collections.Generic;

namespace Web.ShareModels
{
    public class Order
    {
        public int Id {get; set;}

        public string UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public bool status { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
