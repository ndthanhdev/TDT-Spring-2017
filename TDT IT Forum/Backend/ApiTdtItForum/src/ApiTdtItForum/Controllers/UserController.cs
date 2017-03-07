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
using ApiTdtItForum.DTO;
using ApiTdtItForum.Controllers.DTO;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTdtItForum.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        DataContext _db;
        Jwt _jwt;
        UserServices _services;
        readonly IMapper _mapper;

        public UserController(DataContext db, Jwt jwt, UserServices services, IMapper mapper)
        {
            _db = db;
            _jwt = jwt;
            _services = services;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginInfo)
        {
            User innerUser = await _services.Login(loginInfo.Username, loginInfo.PasswordHash);
            var payload = new ResponsePayload();
            if (innerUser == null)
            {
                var isExisted = await _services.IsUsernameExisted(loginInfo.Username);
                payload.StatusCode = (int)(isExisted ? LoginResponseCode.Incorrect : LoginResponseCode.NotExist);
                return Json(payload);
            }

            var jwt = await GenerateJwt(innerUser);

            // Serialize and return the response
            //var response = new
            //{
            //    id_token = jwt
            //};
            payload.Data = jwt;
            return Json(payload);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerInfo)
        {
            var payload = new ResponsePayload();

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Role,RegisteredRoles.User)
            };

            var result = await _services.RegisterUser(_mapper.Map<User>(registerInfo), claims);

            if (result == null)
            {
                if (await _services.IsUsernameExisted(registerInfo.Username))
                {
                    payload.StatusCode = (int)RegisterResponseCode.Existed;
                    return Json(payload);
                }
                else
                {
                    payload.StatusCode = (int)RegisterResponseCode.Incorrect;
                    return Json(payload);
                }
            }

            var jwt = await GenerateJwt(result);
            payload.Data = jwt;
            payload.StatusCode = (int)RegisterResponseCode.Created;
            return Json(payload);
        }

        public async Task<string> GenerateJwt(User user)
        {
            await _db.Entry(user).Collection(model => model.UserClaims).LoadAsync();

            var claims = new List<Claim>();

            // Add username
            claims.Add(new Claim(ClaimTypes.Name, user.Username));

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
