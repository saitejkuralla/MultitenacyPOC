using IMM.MultiTenancy.Extentions.Collections;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.TenantResolveContributers
{
    public class JwtTenantResolveContributor : HttpTenantResolveContributorBase
    {
        public const string ContributorName = "Jwt";

        public override string Name => ContributorName;

        protected override Task<string> GetTenantIdOrNameFromHttpContextOrNullAsync(ITenantResolveContext context, HttpContext httpContext)
        {
            if (httpContext.Request.Headers.IsNullOrEmpty())
            {
                return Task.FromResult((string)null);
            }



            var jwtToken = httpContext.Request.Headers["Authorization"];


            if (jwtToken.Count > 0)
            {
                // remove bearer

                var tokenString = jwtToken.ToString().Substring(7);

                var token = new JwtSecurityToken(jwtEncodedString: tokenString);

                return Task.FromResult(token.Claims.First(f => f.Type == "TenantID").Value);

            }



            return Task.FromResult<string>(null);
        }

        protected virtual void Log(ITenantResolveContext context, string text)
        {
            context
                .ServiceProvider
                .GetRequiredService<ILogger<JwtTenantResolveContributor>>()
                .LogWarning(text);
        }
    }
}
