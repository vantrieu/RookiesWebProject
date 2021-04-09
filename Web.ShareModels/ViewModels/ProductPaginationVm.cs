using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ShareModels.ViewModels
{
    public class ProductPaginationVm
    {
        public List<ProductVm> items { get; set; }

        public int totalCount { get; set; }

        public int pageSize { get; set; }

        public int currentPage { get; set; }

        public int totalPages { get; set; }

        public string previousPage { get; set; }

        public string nextPage { get; set; }
    }
}
