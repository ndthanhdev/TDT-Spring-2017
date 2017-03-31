using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiTdtItForum.Models;
using System.Security.Claims;
using ApiTdtItForum.Security;
using ApiTdtItForum.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTdtItForum.Controllers
{
    [Route("[controller]")]
    public class SeedController : Controller
    {
        UserServices _services;

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
            if (!await _services.IsUsernameExisted("admin"))
            {
                var admin = new User()
                {
                    Username = "admin",
                    PasswordHash = "admin",
                    FullName = "Adminstrator",
                    Faculty = "Information Technology",
                    AdmissionYear = 2014,
                    Email = "adminforum@tdt.edu.vn",
                    Phone = "0123456789"
                };

                var roles = new[] { RegisteredRoles.Adminstrator, RegisteredRoles.User, RegisteredRoles.Moderator };

                await _services.RegisterUser(admin, roles.Select(r => new Claim(ClaimTypes.Role, r)));

            }
        }
    }
}
