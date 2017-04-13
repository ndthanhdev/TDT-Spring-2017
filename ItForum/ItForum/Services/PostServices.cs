using System.Collections.Generic;
using System.Threading.Tasks;
using ItForum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ItForum.Services
{
    public class PostServices
    {
        private readonly DataContext _data;
        private readonly UserServices _userServices;

        public PostServices(DataContext data, UserServices userServices)
        {
            _data = data;
            _userServices = userServices;
        }

        public async Task<List<Post>> GetPostInContainer(string containerId)
        {
            var container = await _data.Containers.Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.ContainerId == containerId);
            if (container == null)
                return null;
            return new List<Post>(container.Posts);
        }

        public async Task<bool> IsPostValid(Post post)
        {
            await Task.Yield();
            if (string.IsNullOrWhiteSpace(post?.Content))
            {
                return false;
            }
            if (await _userServices.GetUserByIdAsync(post.UserId) == null)
            {
                return false;
            }
            return true;
        }

    }
    public static class PostServicesExtensions
    {
        public static void AddPostServices(this IServiceCollection builder)
        {
            builder.AddScoped(typeof(PostServices));
        }
    }
}