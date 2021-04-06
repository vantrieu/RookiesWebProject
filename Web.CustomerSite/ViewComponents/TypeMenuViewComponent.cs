using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.CustomerSite.Services;

namespace Web.CustomerSite.ViewComponents
{
    public class TypeMenuViewComponent : ViewComponent
    {
        private readonly ICategoryApiClient _categoryApiClient;

        public TypeMenuViewComponent(ICategoryApiClient categoryApiClient)
        {
            _categoryApiClient = categoryApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var types = await _categoryApiClient.GetCategory();
            return View(types);
        }
    }
}
