using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ShareModels.ViewModels
{
    public class OrderResponseVM
    {
        public int orderId { get; set; }

        public string fullname { get; set; }
        
        public DateTime orderDate { get; set; }

        public bool status { get; set; }

        public List<OrderDetailResponseVm> products { get; set; }
    }
}
