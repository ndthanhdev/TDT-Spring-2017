using AutoMapper;
using ItForum.Controllers.DTO;
using ItForum.Controllers.DTO.TagController;
using ItForum.Models;
using ItForum.Services;
using ItForum.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItForum.Controllers
{
    [Route("[controller]/[action]")]
    public class TagController : Controller
    {
        TagServices _services;
        UserServices _userServices;
        readonly IMapper _mapper;

        public TagController(TagServices services, UserServices userServices, IMapper mapper)
        {
            _services = services;
            _userServices = userServices;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(RegisteredPolicys.Adminstrator)]
        public async Task<IActionResult> Create([FromBody] Tag tag)
        {
            Payload payload = new Payload();
            if (TagServices.IsDataCorrect(tag))
            {
                payload.StatusCode = (int)TagCreateCode.Incorrect;
            }
            else if (await _services.IsTagExisted(tag))
            {
                payload.StatusCode = (int)TagCreateCode.Existed;
            }
            else
            {
                payload.Data = JsonConvert.SerializeObject(await _services.CreateTag(tag), Formatting.Indented);
            }
            return Json(payload);
        }

        [HttpPost]
        [Authorize(RegisteredPolicys.Adminstrator)]
        public async Task<IActionResult> AddUserTag([FromBody] UserTag data)
        {
            Payload payload = new Payload();

            if (await _userServices.GetUserByIdAsync(data.UserId) == null)
            {
                // user not exist
                payload.StatusCode = (int)TagAddUserTagCode.UserNotExist;
            }
            else if (await _services.GetTagById(data.TagId) == null)
            {
                // tag doesn't exist
                payload.StatusCode = (int)TagAddUserTagCode.TagNotExist;
            }
            else
            {
                // create UserTag 
                await _services.AddUserTag(data.UserId, data.TagId);
                payload.StatusCode = (int)TagAddUserTagCode.Created;
            }

            return Json(payload);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            Payload payload = new Payload();
            payload.Data = await _services.GetAllTags();
            return Json(payload);
        }

    }
}
