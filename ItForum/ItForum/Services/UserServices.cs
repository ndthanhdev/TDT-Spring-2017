using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ItForum.Models;
using ItForum.Services.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ItForum.Services
{
    public class UserServices
    {
        private static readonly Regex StudentIdPattern = new Regex("[0-9,A,B,C]{1}\\d{2}\\d{5}");

        private readonly DataContext _db;
        private readonly JwtServices _jwt;

        public UserServices(DataContext dataContext, JwtServices jwt)
        {
            _db = dataContext;
            _jwt = jwt;
        }

        public async Task<User> RegisterUser(User user, IEnumerable<Claim> claims, bool isVerified = false)
        {
            if (!IsCorrectInfor(user))
                return null;

            if (await GetUserByUserName(user.Username) != null)
                return null;

            user.IsVerified = isVerified;
            await _db.Users.AddAsync(user);
            var jobs = new List<Task>();

            foreach (var claim in claims)
                jobs.Add(_db.UserClaims.AddAsync(new UserClaim
                {
                    UserId = user.UserId,
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value
                }));

            await Task.WhenAll(jobs);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByUserName(string username)
        {
            var userInDb = await _db.Users.FirstOrDefaultAsync(model => model.Username == username);

            return userInDb;
        }

        public async Task<User> Login(string username, string passwordHash)
        {
            var innerUser =
                await _db.Users.FirstOrDefaultAsync(
                    model => model.Username == username && model.PasswordHash == passwordHash);
            return innerUser;
        }

        public static bool IsCorrectInfor(User user)
        {
            if (string.IsNullOrEmpty(user.Username)
                || string.IsNullOrEmpty(user.PasswordHash)
                || string.IsNullOrEmpty(user.FullName)
                || string.IsNullOrEmpty(user.Faculty)
                || string.IsNullOrEmpty(user.Email)
                || string.IsNullOrEmpty(user.Phone))
                return false;
            return true;
        }

        public async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>();

            await _db.Entry(user).Collection(u => u.UserClaims).LoadAsync();
            claims.AddRange(user.UserClaims.Select(uc => uc.ToClaim()));

            claims.Add(new Claim(ClaimTypes.Name, user.UserId));
            return claims;
        }

        public async Task<string> GenerateJwt(User user)
        {
            var claims = await GetClaimsAsync(user);

            var jwtToken = new JwtSecurityToken(
                _jwt.Issuer,
                _jwt.Audience,
                claims,
                _jwt.NotBefore,
                _jwt.Expiration,
                _jwt.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return encodedJwt;
        }

        public async Task<User> GetUserProfile(string userId)
        {
            await Task.Yield();
            var user = await _db.Users.Include(u => u.UserTags)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId);
            user.PasswordHash = "";
            return user;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task VerifyUser(string id)
        {
            var innerUser = await GetUserByIdAsync(id);
            innerUser.IsVerified = !innerUser.IsVerified;
            await _db.SaveChangesAsync();
        }

        public async Task<int> VerifyUsers(IEnumerable<string> ids)
        {
            List<Task> tasks = new List<Task>();
            foreach (var id in ids)
            {
                tasks.Add(Task.Run(async () =>
                {
                    var innerUser = await GetUserByIdAsync(id);
                    innerUser.IsVerified = !innerUser.IsVerified;
                }));
            }
            await Task.WhenAll(tasks);
            await _db.SaveChangesAsync();
            return tasks.Count;
        }


        public async Task<bool> IsTdt(string studentId)
        {
            await Task.Yield();
            return true;
        }

        public async Task<List<User>> GetAllUser()
        {
            return await _db.Users.AsNoTracking().ToListAsync();
        }

        public async Task<List<User>> GetAllUnverifyUser()
        {
            return await _db.Users.AsNoTracking().Where(u => !u.IsVerified).ToListAsync();
        }


        public static bool IsStudentId(string studentId)
        {
            return StudentIdPattern.IsMatch(studentId);
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