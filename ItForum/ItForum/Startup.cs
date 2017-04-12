using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ItForum.Services;
using ItForum.Services.Jwt;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ItForum
{
    public class Startup
    {
        private readonly IConfigurationSection _jwtConfigurationSection;
        private readonly string _connectionString;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            _jwtConfigurationSection = Configuration.GetSection("Jwt");

#if DEBUG
            // This line for local 
            _connectionString = Configuration.GetConnectionString("LocalConnection");
#else
            //This line for server 
            _connectionString = Configuration.GetConnectionString("DefaultConnection");
#endif

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(_connectionString);
            });

            // Add framework services.
            services.AddMvc();

            services.AddRouting();

            services.AddJwtServices(_jwtConfigurationSection[nameof(JwtServices.Issuer)],
                _jwtConfigurationSection[nameof(JwtServices.Audience)],
                _jwtConfigurationSection["Secret"]);

            services.AddAuthorization(ConfiguredAuthorization.Configure);

            services.AddUserServices();

            services.AddTagServices();

            services.AddPostServices();

            services.AddCors();

            services.AddMapperServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtConfigurationSection[nameof(JwtServices.Issuer)],

                ValidateAudience = true,
                ValidAudience = _jwtConfigurationSection[nameof(JwtServices.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtConfigurationSection["Secret"])),

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
