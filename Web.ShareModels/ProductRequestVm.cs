using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ShareModels
{
    public class ProductRequestVm
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantities { get; set; }

        public Int64 Price { get; set; }

        public int CategoryId { get; set; }

        public IFormFile image { get; set; }
    }
}
