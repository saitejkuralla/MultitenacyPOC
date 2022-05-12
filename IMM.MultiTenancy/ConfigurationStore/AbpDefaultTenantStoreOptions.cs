using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.ConfigurationStore
{
    public class AbpDefaultTenantStoreOptions
    {
        public TenantConfiguration[] Tenants { get; set; }

        public AbpDefaultTenantStoreOptions()
        {
            Tenants = new TenantConfiguration[0];
        }
    }
}
