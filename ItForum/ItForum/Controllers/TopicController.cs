using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItForum.Controllers.DTO;
using ItForum.Controllers.DTO.TopicController;
using ItForum.Models;
using ItForum.Services;
using ItForum.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItForum.Controllers
{
    [Route("[controller]/[action]")]
    public class TopicController : Controller
    {
        private readonly TopicServices _topicServices;
        private readonly PostServices _postServices;
        private readonly TagServices _tagServices;

        public TopicController(TopicServices topicServices, PostServices postServices, TagServices tagServices)
        {
            _topicServices = topicServices;
            _postServices = postServices;
            _tagServices = tagServices;
        }


        [Authorize(RegisteredPolicys.User)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Topic topic)
        {
            Payload payload = new Payload();
            if (!await _postServices.IsPostValid(topic.Post))
            {
                // unvalid post
                payload.StatusCode = CreateResponseCode.InvalidPost;
                return Json(payload);
            }
            if (!await _topicServices.IsTopicValid(topic))
            {
                // unvalid topic
                payload.StatusCode = CreateResponseCode.InvalidContainer;
                return Json(payload);
            }
            topic.Post.IsVerified = false;
            payload.Data = (await _topicServices.CreateTopic(topic)).TopicId;
            payload.StatusCode = CreateResponseCode.Ok;
            return Json(payload);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopic()
        {
            Payload payload = new Payload();
            var containers = await _topicServices.GetTopics();
            payload.Data = containers;
            payload.StatusCode = GetAllContainerResponseCode.Ok;
            return Json(payload);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTopicsInTag(string id)
        {
            Payload payload = new Payload();
            if (await _tagServices.GetTagByName(id) == null)
            {
                payload.StatusCode = GetContainersInTagResponseCode.TagNotExist;
                return Json(payload);
            }
            payload.Data = await _topicServices.GetTopicsInTag(id);
            payload.StatusCode = GetContainersInTagResponseCode.Ok;
            return Json(payload);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetTopic(string id)
        {
            Payload payload = new Payload();
            payload.Data = await _topicServices.GetTopicById(id);
            payload.StatusCode = GetTopicCode.Ok;
            if (payload.Data == null)
            {
                payload.StatusCode = GetTopicCode.NotExist;
            }
            return Json(payload);
        }

    }
}
