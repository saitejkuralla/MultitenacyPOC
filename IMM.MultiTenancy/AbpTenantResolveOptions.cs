using IMM.MultiTenancy.TenantResolveContributers;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy
{
    public class AbpTenantResolveOptions
    {
        [NotNull]  //TODO check why do we need to use not null
        public List<ITenantResolveContributor> TenantResolvers { get; }

        public AbpTenantResolveOptions()
        {
            TenantResolvers = new List<ITenantResolveContributor>
            {
                
                new QueryStringTenantResolveContributor(), // TODO need to understand why only CurrentUserTenantResolveContributor added . QueryStringTenantResolveContributoris added by me
                new CurrentUserTenantResolveContributor()
            };
        }
    }
}
