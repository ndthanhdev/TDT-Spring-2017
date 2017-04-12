using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ItForum.Models
{
    public class Point
    {
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }


        public string PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
    }
}
