using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Backend.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantities { get; set; }

        public Int64 Price { get; set; }

        public int ImageId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Rate> Rates { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual FileImage FileImage { get; set; }
    }
}
