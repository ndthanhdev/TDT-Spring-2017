using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.Controllers.SharedObjects.Response
{
    public class ProfileResponse
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Faculty { get; set; }
        public int AdmissionYear { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsVerified { get; set; }
        public List<string> Roles { get; set; }
        public List<string> ManagedTagIds { get; set; }
        
    }
}
