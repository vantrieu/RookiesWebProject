using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Backend.Models
{
    public class FileImage
    {
        public int Id { get; set; }

        public string FileLocation { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
