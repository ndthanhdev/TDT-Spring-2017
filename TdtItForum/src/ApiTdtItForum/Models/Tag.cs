using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.Models
{
    public class Tag
    {
        public string TagId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<ContainerTag> ContainerTags { get; set; }

        public List<UserTag> UserTags { get; set; }
    }
}
