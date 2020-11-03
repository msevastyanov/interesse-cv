using Interesse.DbLayer.Context;
using Interesse.Domain.Entities;
using Interesse.Domain.ViewModels;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services
{
    public class PageCategoryService : IPageCategoryService
    {
        private readonly DataContext _db;
        private readonly IFileService _fileService;

        public PageCategoryService(DataContext db, IFileService fileService)
        {
            _db = db;
            _fileService = fileService;
        }

        public async Task<List<PageCategory>> GetPageCategories()
        {
            return await _db.PageCategory.Include(x => x.Pages).Where(x => !x.IsRemoved).OrderBy(x => x.Sort).ToListAsync();
        }

        public async Task<List<PageCategory>> GetPageCategoriesForMenu()
        {
            return await _db.PageCategory.Where(x => !x.IsRemoved).Where(x => x.IsActive && (x.ShowOnMainMenu || x.ShowOnMenuDropdown)).ToListAsync();
        }

        public async Task<List<PageCategory>> GetPageCategoriesForIndex()
        {
            return await _db.PageCategory.Where(x => !x.IsRemoved).Where(x => x.IsActive && x.ShowHomeCount > 0).OrderBy(x => x.Sort).ToListAsync();
        }

        public async Task<PageCategory> FindPageCategory(int id)
        {
            return await _db.PageCategory.FindAsync(id);
        }

        public async Task<PageCategory> FindPageCategory(string url)
        {
            return await _db.PageCategory.SingleOrDefaultAsync(x => !x.IsRemoved && x.Url == url);
        }

        public async Task<PageCategory> FindPageCategoryWithPages(int id)
        {
            return await _db.PageCategory.Include(x => x.Pages).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PageCategory> AddPageCategory(PageCategory pageCategory)
        {
            pageCategory.CreatedDate = DateTime.Now;
            pageCategory.ChangedDate = DateTime.Now;

            if (pageCategory.File != null)
                pageCategory.PreviewImage = await UpdateFileName(pageCategory.File);

            pageCategory.Sort = _db.PageCategory.Count() + 1;

            await _db.PageCategory.AddAsync(pageCategory);

            await _db.SaveChangesAsync();

            return await Task.FromResult(pageCategory);
        }

        public async Task<PageCategory> UpdatePageCategory(PageCategory pageCategory)
        {
            pageCategory.ChangedDate = DateTime.Now;

            if (pageCategory.File != null)
                pageCategory.PreviewImage = await UpdateFileName(pageCategory.File);

            _db.Update(pageCategory);

            await _db.SaveChangesAsync();

            return await Task.FromResult(pageCategory);
        }

        public async Task<PageCategory> MovePageCategoryUp(int id)
        {
            var pageCategory = await _db.PageCategory.FindAsync(id);
            var anotherPageCategory = await _db.PageCategory.OrderByDescending(x => x.Sort).Where(x => x.Sort < pageCategory.Sort).FirstOrDefaultAsync();

            if (anotherPageCategory == null)
                anotherPageCategory = await _db.PageCategory.OrderByDescending(x => x.Sort).FirstOrDefaultAsync();

            if (anotherPageCategory == null)
                return await Task.FromResult(pageCategory);

            var currentPageCategorySort = pageCategory.Sort;
            pageCategory.Sort = anotherPageCategory.Sort;
            anotherPageCategory.Sort = currentPageCategorySort;

            await _db.SaveChangesAsync();

            return await Task.FromResult(pageCategory);
        }

        public async Task<PageCategory> MovePageCategoryDown(int id)
        {
            var pageCategory = await _db.PageCategory.FindAsync(id);
            var anotherPageCategory = await _db.PageCategory.OrderBy(x => x.Sort).Where(x => x.Sort > pageCategory.Sort).FirstOrDefaultAsync();

            if (anotherPageCategory == null)
                anotherPageCategory = await _db.PageCategory.OrderBy(x => x.Sort).FirstOrDefaultAsync();

            if (anotherPageCategory == null)
                return await Task.FromResult(pageCategory);

            var currentPageCategorySort = pageCategory.Sort;
            pageCategory.Sort = anotherPageCategory.Sort;
            anotherPageCategory.Sort = currentPageCategorySort;

            await _db.SaveChangesAsync();

            return await Task.FromResult(pageCategory);
        }

        public async Task DeletePageCategory(int id)
        {
            var pageCategory = await _db.PageCategory.Include(x => x.Pages).SingleOrDefaultAsync(x => x.Id == id);

            if (pageCategory != null)
                pageCategory.IsRemoved = true;

            pageCategory.Pages.Clear();

            await _db.SaveChangesAsync();
        }

        public async Task AddPageToCategory(int categoryId, int pageId)
        {
            var category = await _db.PageCategory.Include(x => x.Pages).SingleOrDefaultAsync(x => x.Id == categoryId);
            var page = await _db.Page.FindAsync(pageId);

            category.Pages.Add(page);
            page.Sort = category.Pages.Count() + 1;

            await _db.SaveChangesAsync();
        }

        public async Task RemovePageFromCategory(int pageId)
        {
            var page = await _db.Page.Include(x => x.Category).SingleOrDefaultAsync(x => x.Id == pageId);

            page.Category = null;
            page.Sort = 0;

            await _db.SaveChangesAsync();
        }

        private async Task<string> UpdateFileName(IFormFile file)
        {
            var filesPaths = await _fileService.UploadFile(new UploadFileViewModel
            {
                Folder = "pageCategories",
                File = file
            });

            return filesPaths.FirstOrDefault();
        }
    }
}
