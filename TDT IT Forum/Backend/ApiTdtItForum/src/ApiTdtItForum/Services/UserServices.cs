using ApiTdtItForum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiTdtItForum.Services
{
    public class UserServices
    {
        DataContext _db;

        public UserServices(DataContext dataContext)
        {
            _db = dataContext;
        }

        public async Task<CreateUserResult> CreateUser(User user, IEnumerable<Claim> claims, bool IsVerified = false)
        {
            if (IsIncorrectInfor(user))
            {
                return CreateUserResult.Incorrect;
            }

            if(await IsUsernameExisted(user.UserName))
            {
                return CreateUserResult.Existed;
            }

            user.IsVerified = IsVerified;
            await _db.Users.AddAsync(user);
            List<Task> jobs = new List<Task>();

            foreach (var claim in claims)
            {
                jobs.Add(_db.UserClaims.AddAsync(new UserClaim()
                {
                    UserId = user.UserId,
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                }));
            }

            await Task.WhenAll(jobs);
            await _db.SaveChangesAsync();
            return CreateUserResult.Created;
        }

        public async Task<bool> IsUsernameExisted(string username)
        {
            var userInDb = await _db.Users.FirstOrDefaultAsync(model => model.UserName == username);

            return userInDb == null;
        }

        public static bool IsIncorrectInfor(User user)
        {
            if (string.IsNullOrEmpty(user.PasswordHash)
               || string.IsNullOrEmpty(user.FullName)
               || string.IsNullOrEmpty(user.Faculty)
               || string.IsNullOrEmpty(user.Mail)
               || string.IsNullOrEmpty(user.Phone))
            {
                return false;
            }
            return true;
        }

    }



    public static class UserServicesExtensions
    {
        public static void AddUserServices(this IServiceCollection builder)
        {
            builder.AddScoped(typeof(UserServices));
        }
    }
}
