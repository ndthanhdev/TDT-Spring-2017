using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItForum.Services;
using Microsoft.AspNetCore.Mvc;

namespace ItForum.Controllers
{
    public class PostsController : Controller
    {
        private readonly ContainerServices _services;

        public PostsController(ContainerServices services)
        {
            _services = services;
        }
    }
}