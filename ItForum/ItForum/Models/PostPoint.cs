using System.ComponentModel.DataAnnotations.Schema;

namespace ItForum.Models
{
    public class PostPoint
    {
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }


        public string PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
    }
}