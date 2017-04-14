using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItForum.Controllers.DTO.PostController
{
    public class AddPostResponseCode
    {
        public static readonly int Ok = 0;
        public static readonly int PostInvalid = 1;
        public static readonly int ContainerNotExist = 2;
    }
}
