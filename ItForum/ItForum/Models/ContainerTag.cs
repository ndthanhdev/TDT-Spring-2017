using System.ComponentModel.DataAnnotations.Schema;

namespace ItForum.Models
{
    public class ContainerTag
    {
        public string ContainerId { get; set; }

        [ForeignKey(nameof(ContainerId))]
        public Container Container { get; set; }

        public string TagName { get; set; }

        [ForeignKey(nameof(TagName))]
        public Tag Tag { get; set; }
    }
}