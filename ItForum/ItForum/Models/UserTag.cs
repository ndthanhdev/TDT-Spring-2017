using System.ComponentModel.DataAnnotations.Schema;

namespace ItForum.Models
{
    public class UserTag
    {
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public string TagId { get; set; }

        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }
    }
}