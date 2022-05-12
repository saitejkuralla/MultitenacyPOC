using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy
{
    public interface ITenantResolveContributor
    {
        string Name { get; }

        Task ResolveAsync(ITenantResolveContext context);
    }
}
