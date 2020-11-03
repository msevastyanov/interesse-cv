using Interesse.DbLayer.Context;
using Interesse.Domain.Entities;
using Interesse.Domain.ViewModels;
using Interesse.ServiceLayer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services
{
    public class PageService : IPageService
    {
        private readonly DataContext _db;
        private readonly IFileService _fileService;
        private readonly IPageCategoryService _pageCategoryService;

        public PageService(DataContext db, IFileService fileService, IPageCategoryService pageCategoryService)
        {
            _db = db;
            _fileService = fileService;
            _pageCategoryService = pageCategoryService;
        }

        public async Task<List<Page>> GetPages()
        {
            return await _db.Page.Where(x => !x.IsRemoved).ToListAsync();
        }

        public async Task<List<Page>> GetPagesWithoutCategory()
        {
            return await _db.Page.Include(x => x.Category).Where(x => !x.IsRemoved && x.Category == null).ToListAsync();
        }

        public async Task<List<Page>> GetPagesForMenu()
        {
            return await _db.Page.Where(x => !x.IsRemoved && x.IsActive && (x.ShowOnMainMenu || x.ShowOnMenuDropdown)).OrderBy(x => x.Sort).ToListAsync();
        }

        public async Task<List<Page>> GetCategoryPages(int categoryId)
        {
            return await _db.Page.Include(x => x.Category).Where(x => !x.IsRemoved && x.Category.Id == categoryId).OrderBy(x => x.Sort).ToListAsync();
        }

        public async Task<List<Page>> GetCategoryPages(int categoryId, int count)
        {
            var category = await _db.PageCategory.SingleOrDefaultAsync(x => x.Id == categoryId);
            var renderType = category.RenderType;

            return renderType == CategoryRenderType.Post ?
                await _db.Page.Include(x => x.Category).Where(x => !x.IsRemoved && x.Category.Id == categoryId).OrderByDescending(x => x.CreatedDate).Take(count).ToListAsync() :
                await _db.Page.Include(x => x.Category).Where(x => !x.IsRemoved && x.Category.Id == categoryId).OrderBy(x => x.Sort).Take(count).ToListAsync();
        }

        public async Task<List<Page>> GetCategoryPages(string url)
        {
            return await _db.Page.Include(x => x.Category).Where(x => !x.IsRemoved && x.Category.Url == url).OrderBy(x => x.Sort).ToListAsync();
        }

        public async Task<Page> FindPage(int id)
        {
            return await _db.Page.FindAsync(id);
        }

        public async Task<Page> FindPage(string url)
        {
            return await _db.Page.SingleOrDefaultAsync(x => x.Url == url);
        }

        public async Task<Page> AddPage(Page page)
        {
            page.CreatedDate = DateTime.Now;
            page.ChangedDate = DateTime.Now;

            if (page.File != null)
                page.PreviewImage = await UpdateFileName(page.File);

            page.Sort = 0;

            await _db.Page.AddAsync(page);

            await _db.SaveChangesAsync();

            return await Task.FromResult(page);
        }

        public async Task<Page> AddPage(int categoryId, Page page)
        {
            page.CreatedDate = DateTime.Now;
            page.ChangedDate = DateTime.Now;

            if (page.File != null)
                page.PreviewImage = await UpdateFileName(page.File);

            await _db.Page.AddAsync(page);

            await _db.SaveChangesAsync();

            var category = await _pageCategoryService.FindPageCategoryWithPages(categoryId);

            page.Category = category;
            page.Sort = category.Pages.Count() + 1;

            await _db.SaveChangesAsync();

            return await Task.FromResult(page);
        }

        public async Task<Page> UpdatePage(Page page)
        {
            page.ChangedDate = DateTime.Now;

            if (page.File != null)
                page.PreviewImage = await UpdateFileName(page.File);

            _db.Update(page);

            await _db.SaveChangesAsync();

            return await Task.FromResult(page);
        }

        public async Task<Page> MovePageUp(int categoryId, int id)
        {
            var page = await _db.Page.Include(x => x.Category).Where(x => x.Category.Id == categoryId).SingleOrDefaultAsync(x => x.Id == id);
            var anotherPage = await _db.Page.OrderByDescending(x => x.Sort).Where(x => x.Category.Id == categoryId && x.Sort < page.Sort).FirstOrDefaultAsync();

            if (anotherPage == null)
                anotherPage = await _db.Page.Include(x => x.Category).Where(x => x.Category.Id == categoryId).OrderByDescending(x => x.Sort).FirstOrDefaultAsync();

            if (anotherPage == null)
                return await Task.FromResult(page);

            var currentPageSort = page.Sort;
            page.Sort = anotherPage.Sort;
            anotherPage.Sort = currentPageSort;

            await _db.SaveChangesAsync();

            return await Task.FromResult(page);
        }

        public async Task<Page> MovePageDown(int categoryId, int id)
        {
            var page = await _db.Page.Include(x => x.Category).Where(x => x.Category.Id == categoryId).SingleOrDefaultAsync(x => x.Id == id);
            var anotherPage = await _db.Page.OrderBy(x => x.Sort).Where(x => x.Category.Id == categoryId && x.Sort > page.Sort).FirstOrDefaultAsync();

            if (anotherPage == null)
                anotherPage = await _db.Page.Include(x => x.Category).Where(x => x.Category.Id == categoryId).OrderBy(x => x.Sort).FirstOrDefaultAsync();

            if (anotherPage == null)
                return await Task.FromResult(page);

            var currentPageSort = page.Sort;
            page.Sort = anotherPage.Sort;
            anotherPage.Sort = currentPageSort;

            await _db.SaveChangesAsync();

            return await Task.FromResult(page);
        }

        public async Task DeletePage(int id)
        {
            var page = await _db.Page.FindAsync(id);

            if (page != null)
                page.IsRemoved = true;

            await _db.SaveChangesAsync();
        }

        private async Task<string> UpdateFileName(IFormFile file)
        {
            var filesPaths = await _fileService.UploadFile(new UploadFileViewModel
            {
                Folder = "pages",
                File = file
            });

            return filesPaths.FirstOrDefault();
        }
    }
}
