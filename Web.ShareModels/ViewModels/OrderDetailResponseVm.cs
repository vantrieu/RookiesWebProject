using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ShareModels.ViewModels
{
    public class OrderDetailResponseVm
    { 
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Total { get; set; }

        public Int64 Price { get; set; }
    }
}
