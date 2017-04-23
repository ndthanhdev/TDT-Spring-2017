using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItForum.Models
{
    public class CommentPoint
    {
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }


        public string CommentId { get; set; }

        [ForeignKey(nameof(CommentId))]
        public Comment Post { get; set; }
    }
}
