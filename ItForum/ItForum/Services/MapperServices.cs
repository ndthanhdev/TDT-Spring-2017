using System.Linq;
using AutoMapper;
using ItForum.Controllers.DTO.UserController;
using ItForum.Models;
using Microsoft.Extensions.DependencyInjection;

namespace ItForum.Services
{
    public class MapperServices : Profile
    {
        public MapperServices()
        {
            //CreateMap<RegisterRegisterForm, User>();
            //CreateMap<User, GetProfileResponseData>()
            //    .ForMember(pr => pr.ManagedTagIds,
            //        conf => conf
            //            .MapFrom(t => t.UserTags.Select(ut => ut.TagId)));
        }
    }

    public static class MapperServicesExtension
    {
        public static void AddMapperServices(this IServiceCollection builder)
        {
            var configuration = new MapperConfiguration(cfg => { cfg.AddProfile<MapperServices>(); });

            builder.AddSingleton(configuration.CreateMapper());
        }
    }
}