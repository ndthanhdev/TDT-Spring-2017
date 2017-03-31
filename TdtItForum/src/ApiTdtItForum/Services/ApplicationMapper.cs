using ApiTdtItForum.Controllers.SharedObjects.Request;
using ApiTdtItForum.Controllers.SharedObjects.Response;
using ApiTdtItForum.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTdtItForum.Services
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<RegisterRequest, User>();
            CreateMap<User, ProfileResponse>();

        }
    }

    public static class ApplicationMapperExtensions
    {
        public static void AddApplicationMapper(this IServiceCollection builder)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApplicationMapper>();
            });

            builder.AddSingleton<IMapper>(configuration.CreateMapper());
        }
    }
}
