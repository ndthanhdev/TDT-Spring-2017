using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItForum.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ItForum.Services
{
    public class UserClaimServices
    {
        private readonly DataContext _context;

        public UserClaimServices(DataContext context)
        {
            _context = context;
        }

        public async Task UpdateUserClaim(User user)
        {
            _context.UserClaims.RemoveRange(_context.UserClaims.Where(uc=>uc.UserId==user.UserId));
            await _context.SaveChangesAsync();
            await _context.UserClaims.AddRangeAsync(user.UserClaims);
            await _context.SaveChangesAsync();
        }
    }
    public static class UserClaimServicesExtensions
    {
        public static void AddUserClaimServices(this IServiceCollection builder)
        {
            builder.AddScoped(typeof(UserClaimServices));
        }
    }
}
