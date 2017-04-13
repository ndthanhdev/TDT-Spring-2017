using System.Collections.Generic;
using System.Threading.Tasks;
using ItForum.Models;
using Microsoft.EntityFrameworkCore;

namespace ItForum.Services
{
    public class PostServices
    {
        private readonly DataContext _data;

        public PostServices(DataContext data)
        {
            _data = data;
        }

        public async Task<List<Post>> GetPostInContainer(string containerId)
        {
            var container = await _data.Containers.Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.ContainerId == containerId);
            if (container == null)
                return null;
            return new List<Post>(container.Posts);
        }

        public static async Task<bool> IsPostValid(Post post)
        {
            await Task.Yield();
            if (string.IsNullOrWhiteSpace(post?.Content))
            {
                return false;
            }
            return true;
        }
    }
}