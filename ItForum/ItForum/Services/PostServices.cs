using System.Collections.Generic;
using System.Linq;
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
            var container = await _data.Topics.Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.TopicId == containerId);
            return container == null ? null : new List<Post>(container.Posts);
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

        public async Task<Post> AddPostTask(Post post)
        {
            await _data.Posts.AddAsync(post);
            await _data.SaveChangesAsync();
            return post;
        }

        public async Task<Post> GetPostById(string postId)
        {
            return await _data.Posts.FirstOrDefaultAsync(p => p.PostId == postId);
        }

        public async Task VerifyPost(string postId)
        {
            var post = await GetPostById(postId);
            post.IsVerified = true;
            await _data.SaveChangesAsync();
        }

        public async Task<List<Post>> GetUnverifiedPosts(List<Tag> manageTags)
        {
            var topicIds = await _data.TopicTags.Include(tt => tt.Topic)
                 .ThenInclude(t => t.Posts)
                 .Where(tt => manageTags.Exists(t => t.Name == tt.TagName))
                 .Select(tt => tt.TopicId)
                 .ToListAsync();

            return await _data.Posts.Where(p => topicIds.Contains(p.ContainerId) && !p.IsVerified).ToListAsync();
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