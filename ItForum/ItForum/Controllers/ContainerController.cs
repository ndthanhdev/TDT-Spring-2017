using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItForum.Controllers.DTO;
using ItForum.Controllers.DTO.ContainerController;
using ItForum.Models;
using ItForum.Services;
using ItForum.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItForum.Controllers
{
    [Route("[controller]/[action]")]
    public class ContainerController : Controller
    {
        private readonly ContainerServices _containerServices;
        private readonly PostServices _postServices;
        private readonly TagServices _tagServices;

        public ContainerController(ContainerServices containerServices, PostServices postServices, TagServices tagServices)
        {
            _containerServices = containerServices;
            _postServices = postServices;
            _tagServices = tagServices;
        }


        [Authorize(RegisteredPolicys.User)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Container container)
        {
            Payload payload = new Payload();
            if (!await _postServices.IsPostValid(container.Post))
            {
                // unvalid post
                payload.StatusCode = CreateResponseCode.InvalidPost;
                return Json(payload);
            }
            if (!await _containerServices.IsContainerValid(container))
            {
                // unvalid container
                payload.StatusCode = CreateResponseCode.InvalidContainer;
                return Json(payload);
            }
            payload.Data = (await _containerServices.CreateContainer(container)).ContainerId;
            payload.StatusCode = CreateResponseCode.Ok;
            return Json(payload);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllContainer()
        {
            Payload payload = new Payload();
            var containers = await _containerServices.GetContainers();
            payload.Data = containers;
            payload.StatusCode = GetAllContainerResponseCode.Ok;
            return Json(payload);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetContainersInTag(string id)
        {
            Payload payload = new Payload();
            if (await _tagServices.GetTagById(id) == null)
            {
                payload.StatusCode = GetContainersInTagResponseCode.TagNotExist;
                return Json(payload);
            }
            payload.Data = await _containerServices.GetContainerInTag(id);
            payload.StatusCode = GetContainersInTagResponseCode.Ok;
            return Json(payload);
        }
    }
}
