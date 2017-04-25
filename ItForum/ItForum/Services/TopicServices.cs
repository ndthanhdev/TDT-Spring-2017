using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItForum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ItForum.Services
{
    public class TopicServices
    {
        private readonly DataContext _db;
        private readonly PostServices _postServices;
        private readonly TagServices _tagServices;

        public TopicServices(DataContext db, TagServices tagServices, PostServices postServices)
        {
            _db = db;
            _tagServices = tagServices;
            _postServices = postServices;
        }

        public async Task<List<Topic>> GetTopics()
        {
            var containers = await _db.Topics
                .OrderByDescending(c => c.Post.PublishDate)
                .ToListAsync();
            return containers;
        }

        public async Task<List<Topic>> GetTopicsInTag(string tagName)
        {
            var containerIds = await _db.ContainerTags.Where(ct => ct.TagName == tagName).Select(ct => ct.TopicId).ToListAsync();
            if (containerIds == null || containerIds.Count == 0)
                return null;
            return await _db.Topics.Include(c=>c.TopicTags).Where(c => containerIds.Contains(c.TopicId)).ToListAsync();
        }

        public async Task<bool> IsTopicValid(Topic topic)
        {
            await Task.Yield();
            if (string.IsNullOrWhiteSpace(topic.Title))
                return false;

            // check tag exist
            var tagTasks = topic.TopicTags
                ?.Select(containerContainerTag => _tagServices.GetTagByName(containerContainerTag.TagName))
                .ToList();
            if (tagTasks == null || tagTasks.Count == 0)
                return false;

            await Task.WhenAll(tagTasks);
            return tagTasks.All(tagTask => tagTask.Result != null);
        }

        public async Task<Topic> CreateTopic(Topic topic)
        {
            await _db.Topics.AddAsync(topic);
            await _db.SaveChangesAsync();
            return topic;
        }

        public async Task<Topic> GetTopicById(string containterId)
        {
            return await _db.Topics.Include(t=>t.Post)
                .ThenInclude(p=>p.PostPoints)
                .Include(t=>t.Post.Comments)
                .ThenInclude(c=>c.CommentPoints)
                .FirstOrDefaultAsync(c => c.TopicId == containterId);
        }

        public async Task<Topic> AddPost(Post post)
        {
            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();
            return await GetTopicById(post.ContainerId);
        }
    }

    public static class TopicServicesExtensions
    {
        public static void AddTopicServices(this IServiceCollection builder)
        {
            builder.AddScoped(typeof(TopicServices));
        }
    }
}