using System.Collections.Generic;

namespace ItForum.Models
{
    public class Tag
    {
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