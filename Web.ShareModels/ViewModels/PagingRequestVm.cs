using System;
using System.Collections.Generic;
using System.Text;

namespace Web.ShareModels.ViewModels
{
    public class PagingRequestVm
    {
        const int maxPageSize = 20;

        public int pageNumber { get; set; } = 1;

        public int _pageSize { get; set; } = 3;

        public int pageSize
        {

            get { return _pageSize; }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
