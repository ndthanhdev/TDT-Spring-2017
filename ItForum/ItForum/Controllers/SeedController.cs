using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ItForum.Models;
using ItForum.Services;
using ItForum.Services.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace ItForum.Controllers
{
    [Route("[controller]")]
    public class SeedController : Controller
    {
        private readonly UserServices _services;

        public SeedController(UserServices services)
        {
            _services = services;
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
            if (await _services.GetUserByUserName("admin") == null)
            {
                var admin = new User
                {
                    Username = "admin",
                    PasswordHash = "admin",
                    FullName = "Adminstrator",
                    Faculty = "Information Technology",
                    AdmissionYear = 2014,
                    Email = "adminforum@tdt.edu.vn",
                    Phone = "0123456789"
                };

                var roles = new[] {RegisteredRoles.Adminstrator, RegisteredRoles.User, RegisteredRoles.Moderator};

                await _services.RegisterUser(admin, roles.Select(r => new Claim(ClaimTypes.Role, r)));
            }
        }
    }
}