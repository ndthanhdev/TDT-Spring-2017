using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.Models
{
    public class Post
    {
        public string PostId { get; set; }

        public string Content { get; set; }

        public User Author { get; set; }

        public List<Point> Points { get; set; }
    }
}
