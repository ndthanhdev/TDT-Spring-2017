using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItForum.Controllers.DTO;
using ItForum.Models;
using ItForum.Services;
using ItForum.Services.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ItForum.Controllers
{
    [Route("[controller]/[action]")]
    public class CommentController : Controller
    {
        private readonly CommentServices _commentServices;

        public CommentController(CommentServices commentServices)
        {
            _commentServices = commentServices;
        }

        [HttpPost]
        [Authorize(RegisteredPolicys.User)]
        public async Task<IActionResult> AddComment([FromBody]Comment comment)
        {
            Payload payload = new Payload();
            payload.Data = await _commentServices.AddComment(comment);
            payload.StatusCode = 0;
            return Json(payload);
        }

        [HttpGet]
        [Route("{postId}")]
        public async Task<IActionResult> GetComments(string postId)
        {
            Payload payload = new Payload();
            payload.Data = await _commentServices.GetCommentInPost(postId);
            payload.StatusCode = 0;
            return Json(payload);
        }

        [HttpGet]
        public string test()
        {
            return "ok";
        }

        [HttpGet]
        [Route("{commentId}")]
        public async Task<IActionResult> GetComment(string commentId)
        {
            Payload payload = new Payload();
            payload.Data = await _commentServices.GetComment(commentId);
            payload.StatusCode = 0;
            return Json(payload);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="comment"></param>
        /// <returns>OK,Unauth</returns>
        [HttpPost]
        [Authorize(RegisteredPolicys.User)]
        public async Task<IActionResult> UpdateComment(Comment comment)
        {
            Payload payload = new Payload();
            if (comment.UserId != User.Identity.Name)
            {
                payload.StatusCode = 1;

                return Json(payload);
            }
            payload.Data = await _commentServices.UpdateComment(comment);
            payload.StatusCode = 0;
            return Json(payload);
        }

        [HttpGet]
        [Route("{commentId}")]
        [Authorize(RegisteredPolicys.User)]
        public async Task<IActionResult> LikeComment(string commentId)
        {
            Payload payload = new Payload();
            await _commentServices.LikeComment(commentId, User.Identity.Name);
            payload.StatusCode = 0;
            return Json(payload);
        }

        [HttpGet]
        [Route("{commentId}")]
        public async Task<IActionResult> GetCommentPoints(string commentId)
        {
            Payload payload = new Payload();
            payload.Data = await _commentServices.GetCommentPoints(commentId);
            payload.StatusCode = 0;
            return Json(payload);
        }
    }
}
