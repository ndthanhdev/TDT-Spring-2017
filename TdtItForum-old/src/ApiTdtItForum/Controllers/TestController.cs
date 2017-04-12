using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTdtItForum.Controllers
{
    [Route("[controller]")]
    public class TestController : Controller
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(DateTime.Now);
        }
    }
}
