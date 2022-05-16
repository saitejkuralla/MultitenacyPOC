using IMM.MultiTenancy.TenantResolveContributers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IMM.MultiTenancy
{
    public static class TenantResolveContextExtensions
    {
        public static AbpAspNetCoreMultiTenancyOptions GetAbpAspNetCoreMultiTenancyOptions(this ITenantResolveContext context)
        {
            return context.ServiceProvider.GetRequiredService<IOptions<AbpAspNetCoreMultiTenancyOptions>>().Value;
        }
    }

}
