using IMM.MultiTenancy.TenantResolveContributers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IMM.MultiTenancy
{
    public static class TenantResolveContextExtensions
    {
        public static IMMAspNetCoreMultiTenancyOptions GetAbpAspNetCoreMultiTenancyOptions(this ITenantResolveContext context)
        {
            return context.ServiceProvider.GetRequiredService<IOptions<IMMAspNetCoreMultiTenancyOptions>>().Value;
        }
    }

}
