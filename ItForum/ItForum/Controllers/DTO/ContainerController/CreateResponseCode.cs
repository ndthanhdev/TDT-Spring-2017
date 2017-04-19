using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItForum.Controllers.DTO.ContainerController
{
    public static class CreateResponseCode
    {
        public static readonly int Ok = 0;
        public static readonly int InvalidPost = 1;
        public static readonly int InvalidContainer = 2;
    }
}
