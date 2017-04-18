using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ItForum.Models
{
    public class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TagId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public List<ContainerTag> ContainerTags { get; set; }

        public List<UserTag> UserTags { get; set; }

        public bool Equals(Tag obj)
        {
            return TagId == obj.TagId || Name == obj.Name;
        }
    }
}