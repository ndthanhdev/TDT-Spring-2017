using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Faculty { get; set; }
        public int AdmissionYear { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public bool IsVerified { get; set; } = false;
        public List<UserClaim> UserClaims { get; set; }
        public List<Post> Posts { get; set; }
        public List<UserTag> UserTags { get; set; }
    }
}
