using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.ConfigurationStore
{
    public class IMMDefaultTenantStoreOptions
    {
        public TenantConfiguration[] Tenants { get; set; }

        public IMMDefaultTenantStoreOptions()
        {
            Tenants = new TenantConfiguration[0];
        }
    }
}
