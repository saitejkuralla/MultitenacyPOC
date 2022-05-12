using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy
{
    public interface ITenantResolver
    {
        /// <summary>
        /// Tries to resolve current tenant using registered <see cref="ITenantResolveContributor"/> implementations.
        /// </summary>
        /// <returns>
        /// Tenant id, unique name or null (if could not resolve).
        /// </returns>

        Task<TenantResolveResult> ResolveTenantIdOrNameAsync();
    }
}
