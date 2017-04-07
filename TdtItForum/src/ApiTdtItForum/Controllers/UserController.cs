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
using ApiTdtItForum.Controllers.SharedObjects.UserController;
using ApiTdtItForum.Controllers.SharedObject.UserController;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTdtItForum.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        UserServices _services;
        readonly IMapper _mapper;

        public UserController(UserServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            User innerUser = await _services.Login(user.Username, user.PasswordHash);
            var payload = new Payload();
            if (innerUser == null)
            {
                var userInDb = await _services.GetUserByUserName(user.Username);
                payload.StatusCode = (int)(userInDb != null ? LoginResponseCode.Incorrect : LoginResponseCode.NotExist);
                return Json(payload);
            }

            var jwt = await _services.GenerateJwt(innerUser);

            payload.Data = jwt;
            return Json(payload);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var payload = new Payload();

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Role,RegisteredRoles.User)
            };

            var result = await _services.RegisterUser(user, claims);

            if (result == null)
            {
                if (await _services.GetUserByUserName(user.Username) != null)
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
        [Route("{id}")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var payload = new Payload();
            payload.Data = await _services.GetUserProfile(id);
            payload.StatusCode = payload.Data != null ? (int)GetProfileResponseCode.Ok : (int)GetProfileResponseCode.NotExist;
            return Json(payload);
        }

        [HttpGet]
        [Authorize(RegisteredPolicys.User)]
        public async Task<IActionResult> GetProfile()
        {
            var payload = new Payload();
            payload.Data = await _services.GetUserProfile(User.Identity.Name);
            payload.StatusCode = payload.Data != null ? (int)GetProfileResponseCode.Ok : (int)GetProfileResponseCode.NotExist;
            return Json(payload);
        }
    }


}
