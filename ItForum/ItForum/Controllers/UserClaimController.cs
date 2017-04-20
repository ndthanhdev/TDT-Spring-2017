using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItForum.Controllers.DTO;
using ItForum.Controllers.DTO.TagController;
using ItForum.Controllers.DTO.UserClaimController;
using ItForum.Models;
using ItForum.Services;
using ItForum.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItForum.Controllers
{
    [Route("[controller]/[action]")]
    public class UserClaimController:Controller
    {
        private UserClaimServices _userClaimServices;
        private readonly UserServices _userServices;

        public UserClaimController(UserClaimServices userClaimServices, UserServices userServices)
        {
            _userClaimServices = userClaimServices;
            _userServices = userServices;
        }

        [HttpPost]
        [Authorize(RegisteredPolicys.Adminstrator)]
        public async Task<IActionResult> UpdateUserClaimOfUser([FromBody] User data)
        {
            Payload payload = new Payload();

            if (await _userServices.GetUserByIdAsync(data.UserId) == null)
            {
                payload.StatusCode = UpdateUserClaimOfUserCode.NotExist;
                return Json(payload);
            }

            await _userClaimServices.UpdateUserClaim(data);

            return Json(payload);
        }
    }
}
