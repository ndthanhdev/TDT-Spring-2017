using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiTdtItForum.Security;
using ApiTdtItForum.Services;
using AutoMapper;
using ApiTdtItForum.Models;
using ApiTdtItForum.SharedObject;
using ApiTdtItForum.Controllers.SharedObject.ResponseCode;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTdtItForum.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(RegisteredPolicys.Adminstrator)]
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


    }
}
