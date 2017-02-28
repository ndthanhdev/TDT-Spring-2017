using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiTdtItForum.Security;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using ApiTdtItForum.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using ApiTdtItForum.Services;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTdtItForum.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        DataContext _db;
        Jwt _jwt;
        UserServices _services;

        public UserController(DataContext db, Jwt jwt, UserServices services)
        {
            _db = db;
            _jwt = jwt;
            _services = services;
        }
        
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            var innerUser = _db.Users.FirstOrDefault(model => model.UserName == user.Username && model.PasswordHash == user.PasswordHash);

            if (innerUser == null)
                return Unauthorized();

            var jwt = await GenerateJwt(innerUser);

            // Serialize and return the response
            //var response = new
            //{
            //    id_token = jwt
            //};

            return Ok(jwt);
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Role,RegisteredRoles.User)
            };

            var result = await _services.CreateUser(user, claims);
            if (result != CreateUserResult.Created)
            {
                return BadRequest(result);
            }

            var jwt = await GenerateJwt(user);
            return Ok(jwt);
        }

        public async Task<string> GenerateJwt(User user)
        {
            await _db.Entry(user).Collection(model => model.UserClaims).LoadAsync();

            var claims = new List<Claim>();

            // Add username
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            claims.AddRange(user.UserClaims.Select(model => model.ToClaim()));

            var jwtToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                notBefore: _jwt.NotBefore,
                expires: _jwt.Expiration,
                signingCredentials: _jwt.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return encodedJwt;
        }


    }

    public class UserDTO
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }

}
