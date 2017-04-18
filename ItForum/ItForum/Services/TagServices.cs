using System.Collections.Generic;
using System.Threading.Tasks;
using ItForum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ItForum.Services
{
    public class TagServices
    {
        private readonly DataContext _db;

        public TagServices(DataContext db)
        {
            _db = db;
        }

        public async Task<Tag> CreateTag(Tag tag)
        {
            var entry = await _db.AddAsync(tag);
            if (entry.State == EntityState.Added)
            {
                await _db.SaveChangesAsync();
                return tag;
            }
            return null;
        }

        public async Task<bool> IsTagExisted(Tag tag)
        {
            return await _db.Tags.FirstOrDefaultAsync(t => t.Equals(tag)) != null;
        }

        public static bool IsDataCorrect(Tag tag)
        {
            return !string.IsNullOrWhiteSpace(tag.Name) && !string.IsNullOrWhiteSpace(tag.TagId);
        }

        public async Task<bool> IsUserHasTag(string userId, string tagId)
        {
            return await _db.UserTags.FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TagId == tagId) != null;
        }

        public async Task<Tag> GetTagById(string tagId)
        {
            return await _db.Tags.FirstOrDefaultAsync(t => t.TagId == tagId);
        }

        public async Task<UserTag> AddUserTag(string userId, string tagId)
        {
            var userTag = new UserTag
            {
                UserId = userId,
                TagId = tagId
            };
            await _db.UserTags.AddAsync(userTag);
            await _db.SaveChangesAsync();
            return userTag;
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await _db.Tags.ToListAsync();
        }
    }

    public static class TagServicesExtensions
    {
        public static void AddTagServices(this IServiceCollection builder)
        {
            builder.AddScoped(typeof(TagServices));
        }
    }
}