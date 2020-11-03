using Interesse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services.Interfaces
{
    public interface IPageService
    {
        Task<List<Page>> GetPages();
        Task<List<Page>> GetPagesWithoutCategory();
        Task<List<Page>> GetPagesForMenu();
        Task<List<Page>> GetCategoryPages(int categoryId);
        Task<List<Page>> GetCategoryPages(int categoryId, int count);
        Task<List<Page>> GetCategoryPages(string url);
        Task<Page> FindPage(int id);
        Task<Page> FindPage(string url);
        Task<Page> AddPage(Page page);
        Task<Page> AddPage(int categoryId, Page page);
        Task<Page> UpdatePage(Page page);
        Task<Page> MovePageUp(int categoryId, int id);
        Task<Page> MovePageDown(int categoryId, int id);
        Task DeletePage(int id);
    }
}
