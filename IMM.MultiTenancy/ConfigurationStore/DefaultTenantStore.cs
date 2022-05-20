using IMM.MultiTenancy.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.ConfigurationStore
{

    //check AbpMultiTenancyModule for configuration part
    public class DefaultTenantStore : ITenantStore, ITransientDependency
    {
        private readonly IMMDefaultTenantStoreOptions _options;

        public DefaultTenantStore(IOptionsMonitor<IMMDefaultTenantStoreOptions> options)
        {
            _options = options.CurrentValue;
        }

        public Task<TenantConfiguration> FindAsync(string name)
        {
            return Task.FromResult(Find(name));
        }

        public Task<TenantConfiguration> FindAsync(Guid id)
        {
            return Task.FromResult(Find(id));
        }
        public TenantConfiguration Find(string name)
        {
            return _options.Tenants?.FirstOrDefault(t => t.Name == name);
        }

        public TenantConfiguration Find(Guid id)
        {
            return _options.Tenants?.FirstOrDefault(t => t.Id == id);
        }
    }
}
