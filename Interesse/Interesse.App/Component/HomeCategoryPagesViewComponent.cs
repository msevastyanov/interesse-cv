using Interesse.Domain.Entities;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Interesse.App.Component
{
    public class HomeCategoryPagesViewComponent : ViewComponent
    {
        private readonly IPageService _pageService;

        public HomeCategoryPagesViewComponent(IPageService pageService)
        {
            _pageService = pageService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int categoryId, int pagesCount, CategoryRenderType renderType)
        {
            var pages = await _pageService.GetCategoryPages(categoryId, pagesCount);

            return View((pages, renderType));
        }
    }
}
