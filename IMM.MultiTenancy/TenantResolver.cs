using IMM.MultiTenancy.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IMM.MultiTenancy
{
    public class TenantResolver : ITenantResolver, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMMTenantResolveOptions _options;

        public TenantResolver(IOptions<IMMTenantResolveOptions> options, IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }
        public async Task<TenantResolveResult> ResolveTenantIdOrNameAsync()
        {
            var result = new TenantResolveResult();

            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var context = new TenantResolveContext(serviceScope.ServiceProvider);

                foreach (var tenantResolver in _options.TenantResolvers)   // _options.TenantResolvers will get all the resolvers
                {
                    await tenantResolver.ResolveAsync(context);

                    result.AppliedResolvers.Add(tenantResolver.Name);

                    if (context.HasResolvedTenantOrHost())
                    {
                        result.TenantIdOrName = context.TenantIdOrName;
                        break;
                    }
                }
            }
            return result;
        }
    }
}
