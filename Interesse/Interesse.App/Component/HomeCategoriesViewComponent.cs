using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interesse.App.Component
{
    public class HomeCategoriesViewComponent : ViewComponent
    {
        private readonly IPageCategoryService _pageCategoryService;

        public HomeCategoriesViewComponent(IPageCategoryService pageCategoryService)
        {
            _pageCategoryService = pageCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pageCategories = await _pageCategoryService.GetPageCategoriesForIndex();

            return View(pageCategories);
        }
    }
}
