using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ItForum.Services.Jwt
{
    public class ConfiguredAuthorization
    {
        public static void Configure(AuthorizationOptions options)
        {
            options.AddPolicy(RegisteredPolicys.User, policy => policy.RequireClaim(ClaimTypes.Role, RegisteredRoles.User));
            options.AddPolicy(RegisteredPolicys.Moderator, policy => policy.RequireClaim(ClaimTypes.Role, RegisteredRoles.Moderator));
            options.AddPolicy(RegisteredPolicys.Adminstrator, policy => policy.RequireClaim(ClaimTypes.Role, RegisteredRoles.Adminstrator));
        }
    }
}
