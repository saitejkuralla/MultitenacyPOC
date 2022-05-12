using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.TenantResolveContributers
{
    public abstract class TenantResolveContributorBase : ITenantResolveContributor
    {
        public abstract string Name { get; }

        public abstract Task ResolveAsync(ITenantResolveContext context);
    }
}
