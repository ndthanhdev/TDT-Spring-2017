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
        private readonly UserServices _userServices;

        public PostsController(TopicServices topicServices, PostServices postServices, UserServices userServices)
        {
            _topicServices = topicServices;
            _postServices = postServices;
            _userServices = userServices;
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
            if (await _topicServices.GetTopicById(post.TopicId) == null)
            {
                payload.StatusCode = AddPostResponseCode.ContainerNotExist;
                return Json(payload);
            }
            payload.Data = await _postServices.AddPostTask(post);
            payload.StatusCode = AddPostResponseCode.Ok;
            return Json(payload);
        }

        [HttpGet]
        [Route("{containerId}")]
        public async Task<IActionResult> GetPostInTopic(string containerId)
        {
            var payload = new Payload();
            if (await _topicServices.GetTopicById(containerId) == null)
            {
                payload.StatusCode = GetPostInContainerResponseCode.ContainerNotExist;
                return Json(payload);
            }
            payload.Data = await _postServices.GetPostInTopic(containerId);
            payload.StatusCode = GetPostInContainerResponseCode.Ok;
            return Json(payload);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>OK,NotExist</returns>
        [HttpGet]
        [Route("{postId}")]
        public async Task<IActionResult> VerifyPost(string postId)
        {
            var payload = new Payload();
            var post = await _postServices.GetPostById(postId);
            if (post == null)
            {
                payload.StatusCode = 1;
                return Json(payload);
            }
            await _postServices.VerifyPost(postId);
            payload.StatusCode = 0;
            return Json(payload);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postId"></param>
        /// <returns>Ok</returns>
        [HttpGet]
        [Authorize(RegisteredPolicys.Moderator)]
        public async Task<IActionResult> GetUnVerifyPost()
        {
            var payload = new Payload();
            var tags = await _userServices.GetManageTags(User.Identity.Name);
            if (tags == null)
            {
                payload.StatusCode = 1;
                return Json(payload);
            }
            if (tags.Count == 0)
            {
                payload.StatusCode = 2;
                return Json(payload);
            }
            var post = await _postServices.GetUnverifiedPosts(tags);
            payload.Data = post;
            payload.StatusCode = 0;
            return Json(payload);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(RegisteredPolicys.Moderator)]
        public async Task<IActionResult> UpdatePost(Post post)
        {
            var payload = new Payload();
            await _postServices.UpdatePost(post);
            payload.StatusCode = 0;
            return Json(payload);
        }

        [HttpGet]
        [Route("{postId}")]
        [Authorize(RegisteredPolicys.User)]
        public async Task<IActionResult> LikePost(string postId)
        {
            Payload payload = new Payload();
            await _postServices.LikePost(postId, User.Identity.Name);
            payload.StatusCode = 0;
            return Json(payload);
        }

    }
}