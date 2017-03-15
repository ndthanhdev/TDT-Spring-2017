using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.DTO
{
    public class ResponsePayload
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public String Data { get; set; }
    }
}
