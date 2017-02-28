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

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTdtItForum.Controllers
{
    [Route("api/[controller]")]
    public class JwtController : Controller
    {
        DataContext _db;
        Jwt _jwt;
        public JwtController(DataContext db, Jwt jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO user)
        {
            var innerUser = _db.Users.FirstOrDefault(model => model.UserName == user.Username && model.PasswordHash == user.PasswordHash);

            if (innerUser == null)
                return Unauthorized();
            await _db.Entry(innerUser).Collection(model => model.UserClaims).LoadAsync();

            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, innerUser.UserName));
            claims.Add(new Claim(ClaimTypes.Name, innerUser.UserName)); 

            claims.AddRange(innerUser.UserClaims.Select(model => model.ToClaim()));

            var jwt = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                notBefore: _jwt.NotBefore,
                expires: _jwt.Expiration,
                signingCredentials: _jwt.SigningCredentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Serialize and return the response
            var response = new
            {
                id_token = encodedJwt
            };

            var json = JsonConvert.SerializeObject(response, Formatting.Indented);
            return Ok(json);
        }
    }
    public class UserDTO
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
