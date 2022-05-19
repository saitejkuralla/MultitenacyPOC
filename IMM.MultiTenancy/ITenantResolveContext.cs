using IMM.MultiTenancy.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy
{
    public interface ITenantResolveContext: IServiceProviderAccessor
    {
        string TenantIdOrName { get; set; }

        bool Handled { get; set; }
    }
}
