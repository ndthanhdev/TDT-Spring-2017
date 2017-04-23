using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItForum.Models
{
    public class Topic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TopicId { get; set; }

        public string Title { get; set; }

        public string PostId { get; set; }

        [Required]
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }

        [InverseProperty(nameof(Models.Post.Topic))]
        public List<Post> Posts { get; set; }

        public List<TopicTag> TopicTags { get; set; }
    }
}