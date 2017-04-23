using System.ComponentModel.DataAnnotations.Schema;

namespace ItForum.Models
{
    public class TopicTag
    {
        public string TopicId { get; set; }

        [ForeignKey(nameof(TopicId))]
        public Topic Topic { get; set; }

        public string TagName { get; set; }

        [ForeignKey(nameof(TagName))]
        public Tag Tag { get; set; }
    }
}