using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ShareModels.ViewModels
{
    public class OrderVm
    {
        public int orderId { get; set; }
        public int ProductId { get; set; }

        public string Name { get; set; }

        public DateTimeOffset OrderDate { get; set; }

        public Int64 Price { get; set; }

        public string ImgUrl { get; set; }

        public bool Status { get; set; }
    }
}
