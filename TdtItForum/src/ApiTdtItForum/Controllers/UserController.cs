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
using ApiTdtItForum.SharedObject;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using ApiTdtItForum.Controllers.SharedObjects.Request;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTdtItForum.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        DataContext _db;
        UserServices _services;
        readonly IMapper _mapper;

        public UserController(DataContext db, UserServices services, IMapper mapper)
        {
            _db = db;
            _services = services;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginInfo)
        {
            User innerUser = await _services.Login(loginInfo.Username, loginInfo.PasswordHash);            
            var payload = new Payload();
            if (innerUser == null)
            {
                var isExisted = await _services.IsUsernameExisted(loginInfo.Username);
                payload.StatusCode = (int)(isExisted ? LoginResponseCode.Incorrect : LoginResponseCode.NotExist);
                return Json(payload);
            }

            var jwt = await _services.GenerateJwt(innerUser);

            payload.Data = jwt;
            return Json(payload);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerInfo)
        {
            var payload = new Payload();

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Role,RegisteredRoles.User)
            };

            var userInfo = _mapper.Map<User>(registerInfo);
            var result = await _services.RegisterUser(userInfo, claims);

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
            
            payload.StatusCode = (int)RegisterResponseCode.Created;
            return Json(payload);
        }

        [HttpGet]
        [Authorize(RegisteredPolicys.User)]
        [Route("{id}")]
        public async Task<IActionResult> GetProfile(int id)
        {
            return Ok(id);
        }

        [HttpGet]
        [Authorize(RegisteredPolicys.User)]
        public async Task<IActionResult> GetProfile()
        {
            var result = await _services.GetUserProfile(User.Identity.Name);
            return Ok(User.Identity.Name);
        }
    }


}
