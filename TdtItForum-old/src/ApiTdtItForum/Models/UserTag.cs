using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.Models
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
