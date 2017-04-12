using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItForum.Models
{
    public class Comment
    {
        public string CommentId { get; set; }

        public DateTime PublishDate { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public string PostId { get; set; }

        [Required]
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
    }
}
