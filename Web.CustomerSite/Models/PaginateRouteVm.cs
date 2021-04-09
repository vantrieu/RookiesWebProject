using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.CustomerSite.Models
{
    public class PaginateRouteVm
    {

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public int totalCount { get; set; }

        public int pageSize { get; set; }

        public int currentPage { get; set; }

        public int totalPages { get; set; }

        public string previousPage { get; set; }

        public string nextPage { get; set; }
    }
}
