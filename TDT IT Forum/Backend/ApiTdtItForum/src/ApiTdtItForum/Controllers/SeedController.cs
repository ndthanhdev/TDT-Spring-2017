using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiTdtItForum.Models;
using ApiTdtItForum;
using System.Security.Claims;
using ApiTdtItForum.Security;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTdtItForum.Controllers
{
    [Route("api/[controller]")]
    public class SeedController : Controller
    {
        DataContext _db;

        public SeedController(DataContext dataContext)
        {
            _db = dataContext;
        }

        // GET api/values/5
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await SeedUser();
            return Ok("Done");
        }

        private async Task SeedUser()
        {
            if (_db.Users.FirstOrDefault(model => model.UserName == "admin") == null)
            {
                var admin = new User()
                {
                    UserName = "admin",
                    PasswordHash = "21232f297a57a5a743894a0e4a801fc3",
                    FullName = "Adminstrator",
                    Faculty = "Information Technology",
                    AdmissionYear = 2014,
                    Mail = "adminforum@tdt.edu.vn",
                    Phone = "0123456789",
                };

                await _db.Users.AddAsync(admin);
                List<Task> jobs = new List<Task>();

                var roles = new[] { RegisteredRoles.Adminstrator, RegisteredRoles.User, RegisteredRoles.Moderator };

                foreach (var role in roles)
                {
                    jobs.Add(_db.UserClaims.AddAsync(new UserClaim()
                    {
                        ClaimType = ClaimTypes.Role,
                        ClaimValue = role,
                        UserId = admin.UserId
                    }));
                }

                await Task.WhenAll(jobs);
                await _db.SaveChangesAsync();
            }
        }
    }
}
