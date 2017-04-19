using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItForum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ItForum.Services
{
    public class ContainerServices
    {
        private readonly DataContext _db;
        private readonly PostServices _postServices;
        private readonly TagServices _tagServices;

        public ContainerServices(DataContext db, TagServices tagServices, PostServices postServices)
        {
            _db = db;
            _tagServices = tagServices;
            _postServices = postServices;
        }

        public async Task<List<Container>> GetContainers()
        {
            var containers = await _db.Containers
                .OrderByDescending(c => c.Post.PublishDate)
                .ToListAsync();
            return containers;
        }

        public async Task<List<Container>> GetContainerInTag(string tagId)
        {
            var tag = await _db.Tags.Include(t => t.ContainerTags)
                .ThenInclude(ct => ct.Container)
                .FirstOrDefaultAsync(t => t.Name == tagId);
            if (tag == null)
                return null;
            return new List<Container>(tag.ContainerTags.Select(ct => ct.Container));
        }

        public async Task<bool> IsContainerValid(Container container)
        {
            await Task.Yield();
            if (string.IsNullOrWhiteSpace(container.Title))
                return false;

            // check tag exist
            var tagTasks = container.ContainerTags
                ?.Select(containerContainerTag => _tagServices.GetTagById(containerContainerTag.ContainerId))
                .ToList();
            if (tagTasks == null || tagTasks.Count == 0)
                return false;

            await Task.WhenAll(tagTasks);
            return tagTasks.All(tagTask => tagTask.Result != null);
        }

        public async Task<Container> CreateContainer(Container container)
        {
            await _db.Containers.AddAsync(container);
            await _db.SaveChangesAsync();
            return container;
        }

        public async Task<Container> GetContainerById(string containterId)
        {
            return await _db.Containers.FirstOrDefaultAsync(c => c.ContainerId == containterId);
        }

        public async Task<Container> AddPost(Post post)
        {
            await _db.Posts.AddAsync(post);
            await _db.SaveChangesAsync();
            return await GetContainerById(post.ContainerId);
        }
    }

    public static class ContainerServicesExtensions
    {
        public static void AddContainerServices(this IServiceCollection builder)
        {
            builder.AddScoped(typeof(ContainerServices));
        }
    }
}