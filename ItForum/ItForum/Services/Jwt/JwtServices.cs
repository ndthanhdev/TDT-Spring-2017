using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItForum.Services.Jwt
{
    public class JwtServices
    {
        private static readonly TimeSpan VALIDFOR = TimeSpan.FromDays(1);
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public DateTime NotBefore { get; set; } = DateTime.UtcNow;

        public DateTime Expiration => NotBefore.Add(VALIDFOR);

        public SigningCredentials SigningCredentials { get; set; }
    }

    public static class JwtServicesExtensions
    {
        public static IServiceCollection AddJwtServicesExtensions(this IServiceCollection builder, string issuer, string audience, string secretKey)
        {
            builder.AddTransient(service =>
            {
                var jwt = new JwtServices();
                jwt.Issuer = issuer;
                jwt.Audience = audience;
                jwt.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)), SecurityAlgorithms.HmacSha256);
                return jwt;
            });
            return builder;
        }
    }
}
