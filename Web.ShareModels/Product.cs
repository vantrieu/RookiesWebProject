using System;
using System.Collections.Generic;

namespace Web.ShareModels
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantities { get; set; }

        public Int64 Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }

        public virtual ICollection<ProductFileImage> ProductFileImages { get; set; }
    }
}
