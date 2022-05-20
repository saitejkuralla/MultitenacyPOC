using IMM.MultiTenancy;
using IMM.MultiTenancy.DependencyInjection;
using IMM.MultiTenancy.TenantResolveContributers;
using Microsoft.Extensions.Options;

namespace IMM.Core.API
{
    public class MultiTenancyMiddleware : IMiddleware, ITransientDependency
    {
        private readonly ITenantConfigurationProvider _tenantConfigurationProvider;
        private readonly ICurrentTenant _currentTenant;
        private readonly IMMAspNetCoreMultiTenancyOptions _options;
        private readonly ITenantResolveResultAccessor _tenantResolveResultAccessor;

        public MultiTenancyMiddleware(
            ITenantConfigurationProvider tenantConfigurationProvider,
            ICurrentTenant currentTenant,
            IOptions<IMMAspNetCoreMultiTenancyOptions> options,
            ITenantResolveResultAccessor tenantResolveResultAccessor)
        {
            _tenantConfigurationProvider = tenantConfigurationProvider;
            _currentTenant = currentTenant;
            _tenantResolveResultAccessor = tenantResolveResultAccessor;
            _options = options.Value;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            TenantConfiguration tenant;
            try
            {
                tenant = await _tenantConfigurationProvider.GetAsync(saveResolveResult: true);
            }
            catch (Exception e)
            {
                await _options.MultiTenancyMiddlewareErrorPageBuilder(context, e);
                return;
            }

            if (tenant?.Id != _currentTenant.Id)
            {
                using (_currentTenant.Change(tenant?.Id, tenant?.Name, tenant?.ConnectionStrings))
                {
                    await next(context);
                }
            }
            else
            {
                await next(context);
            }

            
        }
    }
}
