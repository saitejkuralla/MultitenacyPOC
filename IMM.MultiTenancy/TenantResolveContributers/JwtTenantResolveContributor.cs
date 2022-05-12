using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.TenantResolveContributers
{
    public class JwtTenantResolveContributor: HttpTenantResolveContributorBase
    {
        public const string ContributorName = "Domain";

        public override string Name => ContributorName;

        protected override Task<string> GetTenantIdOrNameFromHttpContextOrNullAsync([NotNull] ITenantResolveContext context, [NotNull] HttpContext httpContext)
        {
            throw new NotImplementedException();
        }
    }
}
