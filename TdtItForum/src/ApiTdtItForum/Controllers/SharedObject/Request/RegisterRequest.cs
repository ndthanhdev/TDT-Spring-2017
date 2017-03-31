using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.Controllers.SharedObjects.Request
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public string Faculty { get; set; }
        public int AdmissionYear { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
