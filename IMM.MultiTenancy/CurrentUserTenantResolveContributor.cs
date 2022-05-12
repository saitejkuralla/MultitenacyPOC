using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMM.MultiTenancy.Security;
using Microsoft.Extensions.DependencyInjection;

namespace IMM.MultiTenancy
{
    public class CurrentUserTenantResolveContributor : TenantResolveContributorBase
    {
        public const string ContributorName = "CurrentUser";

        public override string Name => ContributorName;

        public override Task ResolveAsync(ITenantResolveContext context)
        {
            var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
            if (currentUser.IsAuthenticated)
            {
                context.Handled = true;
                context.TenantIdOrName = currentUser.TenantId?.ToString();
            }

            return Task.CompletedTask;
        }
    }
}
