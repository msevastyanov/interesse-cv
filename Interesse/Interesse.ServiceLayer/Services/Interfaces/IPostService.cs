using Interesse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interesse.ServiceLayer.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetPosts();
        Task<List<Post>> GetPosts(int count);
        Task<Post> FindPost(int id);
        Task<Post> AddPost(Post post);
        Task<Post> UpdatePost(Post post);
        Task DeletePost(int id);
    }
}
