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
        private readonly TagServices _tagServices;

        public ContainerServices(DataContext db, TagServices tagServices)
        {
            _db = db;
            _tagServices = tagServices;
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
                .FirstOrDefaultAsync(t => t.TagId == tagId);
            if (tag == null)
                return null;
            return new List<Container>(tag.ContainerTags.Select(ct => ct.Container));
        }

        public async Task<bool> IsContainerValid(Container container)
        {
            await Task.Yield();
            if (string.IsNullOrWhiteSpace(container.Title))
                return false;
            if (await PostServices.IsPostValid(container.Post))
                return false;
            var tagTasks = container.ContainerTags
                ?.Select(containerContainerTag => _tagServices.GetTagById(containerContainerTag.ContainerId))
                .ToList();
            if (tagTasks == null) return false;
            await Task.WhenAll(tagTasks);
            return tagTasks.All(tagTask => tagTask.Result != null);
        }

        public async Task<Container> CreateContainerTask(Container container)
        {
            if (await IsContainerValid(container))
                return null;
            await _db.Containers.AddAsync(container);
            return container;
        }
    }

    public static class PostServicesExtensions
    {
        public static void AddPostServices(this IServiceCollection builder)
        {
            builder.AddScoped(typeof(ContainerServices));
        }
    }
}