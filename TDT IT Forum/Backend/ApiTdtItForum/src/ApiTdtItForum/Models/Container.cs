using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.Models
{
    public class Container
    {
        public string ContainerId { get; set; }

        public string PostId { get; set; }

        [Required]
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }

        [InverseProperty(nameof(Models.Post.Container))]
        public List<Post> Posts { get; set; }

        public List<ContainerTag> ContainerTags { get; set; }
    }
}
