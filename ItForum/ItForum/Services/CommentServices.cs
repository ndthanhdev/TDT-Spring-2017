using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItForum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ItForum.Services
{
    public class CommentServices
    {
        private readonly DataContext _dataContext;

        public CommentServices(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Comment> AddComment(Comment comment)
        {
            await _dataContext.Comments.AddAsync(comment);
            await _dataContext.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> GetComment(string commentId)
        {
            return await _dataContext.Comments.FirstOrDefaultAsync(c => c.CommentId == commentId);
        }

        public async Task<List<Comment>> GetCommentInPost(string postId)
        {
            return await _dataContext.Comments.Where(c => c.PostId == postId).ToListAsync();
        }

        public async Task<Comment> UpdateComment(Comment comment)
        {
            var innerComment = await _dataContext.Comments.FirstOrDefaultAsync(c=> c.CommentId == comment.CommentId);
            innerComment.Content = comment.Content;
            innerComment.PublishDate = DateTime.Now;
            await _dataContext.SaveChangesAsync();
            return innerComment;
        }

        public async Task LikeComment(string commentId, string userId)
        {
            var innerPoint =
                await _dataContext.CommentPoints.FirstOrDefaultAsync(pp => pp.CommentId == commentId && pp.UserId == userId);
            if (innerPoint == null)
            {
                await _dataContext.CommentPoints.AddAsync(new CommentPoint()
                {
                    CommentId = commentId,
                    UserId = userId
                });
            }
            else
            {
                _dataContext.CommentPoints.Remove(innerPoint);
            }
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<CommentPoint>> GetCommentPoints(string commentId)
        {
            return await _dataContext.CommentPoints.Where(pp => pp.CommentId == commentId).ToListAsync();
        }
    }
    public static class CommentServicesExtensions
    {
        public static void AddCommentServices(this IServiceCollection builder)
        {
            builder.AddScoped(typeof(CommentServices));
        }
    }
}
