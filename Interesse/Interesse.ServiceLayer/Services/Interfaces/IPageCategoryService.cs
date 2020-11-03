using Interesse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services.Interfaces
{
    public interface IPageCategoryService
    {
        Task<List<PageCategory>> GetPageCategories();
        Task<List<PageCategory>> GetPageCategoriesForMenu();
        Task<List<PageCategory>> GetPageCategoriesForIndex();
        Task<PageCategory> FindPageCategory(int id);
        Task<PageCategory> FindPageCategory(string url);
        Task<PageCategory> FindPageCategoryWithPages(int id);
        Task<PageCategory> AddPageCategory(PageCategory pageCategory);
        Task<PageCategory> UpdatePageCategory(PageCategory pageCategory);
        Task<PageCategory> MovePageCategoryUp(int id);
        Task<PageCategory> MovePageCategoryDown(int id);
        Task DeletePageCategory(int id);
        Task AddPageToCategory(int categoryId, int pageId);
        Task RemovePageFromCategory(int pageId);
    }
}
