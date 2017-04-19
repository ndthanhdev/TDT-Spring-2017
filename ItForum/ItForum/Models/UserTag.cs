using System.ComponentModel.DataAnnotations.Schema;

namespace ItForum.Models
{
    public class UserTag
    {
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public string TagName { get; set; }

        [ForeignKey(nameof(TagName))]
        public Tag Tag { get; set; }
    }
}