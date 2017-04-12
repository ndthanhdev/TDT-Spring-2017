using AutoMapper;
using ItForum.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItForum.Services
{
    public class MapperServices : Profile
    {
        public MapperServices()
        {
            //CreateMap<RegisterRegisterForm, User>();
            CreateMap<User, GetProfileResponseData>()
                .ForMember(pr => pr.ManagedTagIds,
                conf => conf
                .MapFrom(t => t.UserTags.Select(ut => ut.TagId)));
        }
    }

    public static class MapperServicesExtension
    {
        public static void AddMapperServices(this IServiceCollection builder)
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MapperServices>();
            });

            builder.AddSingleton<IMapper>(configuration.CreateMapper());
        }
    }
}
