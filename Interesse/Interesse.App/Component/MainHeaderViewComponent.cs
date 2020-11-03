using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interesse.App.Component
{
    public class MainHeaderViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;
        private readonly IPageCategoryService _pageCategoryService;

        public MainHeaderViewComponent(IPageService pageService, IPageCategoryService pageCategoryService)
        {
            _pageService = pageService;
            _pageCategoryService = pageCategoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var pages = await _pageService.GetPagesForMenu();
            var pageCategories = await _pageCategoryService.GetPageCategoriesForMenu();

            return View((pageCategories, pages));
        }
    }
}
