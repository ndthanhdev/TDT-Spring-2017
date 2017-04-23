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
        private readonly TopicServices _topicServices;
        private readonly PostServices _postServices;

        public PostsController(TopicServices topicServices, PostServices postServices)
        {
            _topicServices = topicServices;
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
            if (await _topicServices.GetTopicById(post.ContainerId) == null)
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
            if (await _topicServices.GetTopicById(containerId) == null)
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