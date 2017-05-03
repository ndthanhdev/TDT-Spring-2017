using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItForum.Models
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PostId { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public string TopicId { get; set; }

        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }

        public bool IsVerified { get; set; }

        public List<PostPoint> PostPoints { get; set; }

        public List<Comment> Comments { get; set; }

        public DateTime PublishDate { get; set; }
    }
}