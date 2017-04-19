using System.Threading.Tasks;
using AutoMapper;
using ItForum.Controllers.DTO;
using ItForum.Controllers.DTO.TagController;
using ItForum.Models;
using ItForum.Services;
using ItForum.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ItForum.Controllers
{
    [Route("[controller]/[action]")]
    public class TagController : Controller
    {
        private readonly IMapper _mapper;
        private readonly TagServices _services;
        private readonly UserServices _userServices;

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
            var payload = new Payload();
            if (!TagServices.IsDataCorrect(tag))
                payload.StatusCode = (int) TagCreateCode.Incorrect;
            else if (await _services.IsTagExisted(tag))
                payload.StatusCode = (int) TagCreateCode.Existed;
            else
                payload.Data = JsonConvert.SerializeObject(await _services.CreateTag(tag), Formatting.Indented);
            return Json(payload);
        }

        [HttpPost]
        [Authorize(RegisteredPolicys.Adminstrator)]
        public async Task<IActionResult> AddUserTag([FromBody] UserTag data)
        {
            var payload = new Payload();

            if (await _userServices.GetUserByIdAsync(data.UserId) == null)
            {
                // user not exist
                payload.StatusCode = (int) TagAddUserTagCode.UserNotExist;
            }
            else if (await _services.GetTagById(data.TagName) == null)
            {
                // tag doesn't exist
                payload.StatusCode = (int) TagAddUserTagCode.TagNotExist;
            }
            else
            {
                // create UserTag 
                await _services.AddUserTag(data.UserId, data.TagName);
                payload.StatusCode = (int) TagAddUserTagCode.Created;
            }

            return Json(payload);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTag()
        {
            var payload = new Payload()
            {
                Data = await _services.GetAllTags(),
                StatusCode = GetAllTagCode.Ok
            };
            return Json(payload);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTagById(string id)
        {
            Payload payload = new Payload {Data = await _services.GetTagById(id)};
            payload.StatusCode = payload.Data == null ? GetTagByIdCode.NotExist : GetTagByIdCode.Ok;
            return Json(payload);
        }
    }
}