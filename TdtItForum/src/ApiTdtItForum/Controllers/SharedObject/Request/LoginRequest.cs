using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.Controllers.SharedObjects.Request
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
