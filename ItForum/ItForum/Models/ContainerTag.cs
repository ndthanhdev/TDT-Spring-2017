using System.ComponentModel.DataAnnotations.Schema;

namespace ItForum.Models
{
    public class ContainerTag
    {
        public string ContainerId { get; set; }

        [ForeignKey(nameof(ContainerId))]
        public Container Container { get; set; }

        public string TagId { get; set; }

        [ForeignKey(nameof(TagId))]
        public Tag Tag { get; set; }
    }
}