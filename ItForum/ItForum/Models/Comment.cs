using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ItForum.Models
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CommentId { get; set; }

        public DateTime PublishDate { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public List<CommentPoint> CommentPoints { get; set; }


        [Required]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public string PostId { get; set; }

        [Required]
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
    }
}