using ApiTdtItForum.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.Services
{
    public class TagServices
    {
        readonly DataContext _db;
        readonly IMapper _mapper;

        public TagServices(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Tag> CreateTag(Tag tag)
        {
            var entry = await _db.Tags.AddAsync(tag);
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

        public async Task<bool> IsUserHasTag(string userId, string TagId)
        {
            return await _db.UserTags.FirstOrDefaultAsync(ut => ut.UserId == userId && ut.TagId == TagId) != null;
        }

        public async Task<Tag> GetTagById(string tagId)
        {
            return await _db.Tags.FirstOrDefaultAsync(t => t.TagId == tagId);
        }

        public async Task<UserTag> AddUserTag(string userId, string tagId)
        {
            UserTag userTag = new UserTag()
            {
                UserId = userId,
                TagId = tagId
            };
            await _db.UserTags.AddAsync(userTag);
            await _db.SaveChangesAsync();
            return userTag;
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
