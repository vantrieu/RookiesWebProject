using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Backend.Models
{
    public class ProductFileImage
    {
        public int ProductId {get; set;}

        public int FileImageId { get; set; }

        public FileImage FileImage { get; set; }
    }
}
