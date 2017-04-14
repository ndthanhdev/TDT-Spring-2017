using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItForum.Controllers.DTO;
using ItForum.Controllers.DTO.PostController;
using ItForum.Models;
using ItForum.Services;
using ItForum.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItForum.Controllers
{
    public class PostsController : Controller
    {
        private readonly ContainerServices _containerServices;
        private readonly PostServices _postServices;

        public PostsController(ContainerServices containerServices, PostServices postServices)
        {
            _containerServices = containerServices;
            _postServices = postServices;
        }

        [HttpPost]
        [Authorize(RegisteredPolicys.User)]
        public async Task<IActionResult> AddPost(Post post)
        {
            var payload = new Payload();
            if (!await _postServices.IsPostValid(post))
            {
                payload.StatusCode = AddPostResponseCode.PostInvalid;
                return Json(payload);
            }
            if (await _containerServices.GetContainerById(post.ContainerId) == null)
            {
                payload.StatusCode = AddPostResponseCode.ContainerNotExist;
                return Json(payload);
            }
            payload.Data = await _postServices.AddPostTask(post);
            payload.StatusCode = AddPostResponseCode.Ok;
            return Json(payload);
        }

        [Route("{containerId}")]
        public async Task<IActionResult> GetPostInContainer(string containerId)
        {
            var payload = new Payload();
            if (await _containerServices.GetContainerById(containerId) == null)
            {
                payload.StatusCode = GetPostInContainerResponseCode.ContainerNotExist;
                return Json(payload);
            }
            payload.Data = await _postServices.GetPostInContainer(containerId);
            payload.StatusCode = GetPostInContainerResponseCode.Ok;
            return Json(payload);
        }

    }
}