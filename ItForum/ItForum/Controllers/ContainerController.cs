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

namespace ItForum.Controllers
{
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
            if (await _postServices.IsPostValid(container.Post))
            {
                // unvalid post
                payload.StatusCode = CreateResponseCode.UnvalidPost;
                return Json(payload);
            }
            if (await _containerServices.IsContainerValid(container))
            {
                // unvalid container
                payload.StatusCode = CreateResponseCode.UnvalidContainer;
                return Json(payload);
            }
            payload.Data = await _containerServices.CreateContainer(container);
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
        public async Task<IActionResult> GetContainersInTag(Tag tag)
        {
            Payload payload = new Payload();
            if (await _tagServices.GetTagById(tag.TagId) == null)
            {
                payload.StatusCode = GetContainersInTagResponseCode.TagNotExist;
                return Json(payload);
            }
            payload.Data = await _containerServices.GetContainerInTag(tag.TagId);
            payload.StatusCode = GetContainersInTagResponseCode.Ok;
            return Json(payload);
        }
    }
}
