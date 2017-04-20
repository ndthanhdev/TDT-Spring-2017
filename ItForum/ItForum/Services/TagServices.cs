using System.Collections.Generic;
using System.Linq;
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

        public async Task<bool> IsTagExisted(string tagName)
        {
            return await _db.Tags.FirstOrDefaultAsync(t => t.Name == tagName) != null;
        }

        public static bool IsDataCorrect(Tag tag)
        {
            return !string.IsNullOrWhiteSpace(tag.Name);
        }

        public async Task<bool> IsUserHasTag(string userId, string tagId)
        {
            return await _db.UserTags.FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TagName == tagId) != null;
        }

        public async Task<Tag> GetTagByName(string tagId)
        {
            return await _db.Tags.FirstOrDefaultAsync(t => t.Name == tagId);
        }

        public async Task<UserTag> AddUserTag(string userId, string tagId)
        {
            var userTag = new UserTag
            {
                UserId = userId,
                TagName = tagId
            };
            await _db.UserTags.AddAsync(userTag);
            await _db.SaveChangesAsync();
            return userTag;
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await _db.Tags.ToListAsync();
        }

        public async Task UpdateUserTags(User user)
        {
            _db.UserTags.RemoveRange(_db.UserTags.Where(ut => ut.UserId == user.UserId));
            await _db.SaveChangesAsync();
            await _db.UserTags.AddRangeAsync(user.UserTags);
            await _db.SaveChangesAsync();
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