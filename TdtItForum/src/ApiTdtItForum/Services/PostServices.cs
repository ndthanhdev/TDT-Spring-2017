using ApiTdtItForum.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.Services
{
    public class PostServices
    {
        private DataContext _db;

        public PostServices(DataContext db)
        {
            _db = db;
        }

        //public async Task<List<Post>> GetPosts()
        //{
            
        //}
    }

    public static class PostServicesExtensions
    {
        public static void AddTagServices(this IServiceCollection builder)
        {
            builder.AddScoped(typeof(PostServices));
        }
    }
}
