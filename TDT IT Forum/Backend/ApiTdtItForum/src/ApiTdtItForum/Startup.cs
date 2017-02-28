using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ApiTdtItForum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.IdentityModel.Tokens.Jwt;
using ApiTdtItForum.Security;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiTdtItForum
{
    public class Startup
    {
        private readonly IConfigurationSection _jwtConfigurationSection;
        private readonly string _connectionString;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            _jwtConfigurationSection = Configuration.GetSection(nameof(Jwt));

            // This line for local 
            _connectionString = Configuration.GetConnectionString("LocalConnection");

            // This line for server 
            //_connectionString = Configuration.GetConnectionString("DefaultConnection");

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(_connectionString);
            });

            services.AddJwt(_jwtConfigurationSection[nameof(Jwt.Issuer)],
                _jwtConfigurationSection[nameof(Jwt.Audience)],
                _jwtConfigurationSection["Secret"]);

            services.AddAuthorization(ConfiguredAuthorization.Configure);            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtConfigurationSection[nameof(Jwt.Issuer)],

                ValidateAudience = true,
                ValidAudience = _jwtConfigurationSection[nameof(Jwt.Audience)],

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

            app.UseClaimsTransformation();

            app.UseMvcWithDefaultRoute();
        }
    }
}
