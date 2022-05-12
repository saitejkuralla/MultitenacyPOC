using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy
{
    /* A null Current indicates that we haven't set it explicitly.
     * A null Current.TenantId indicates that we have set null tenant id value explicitly.
     * A non-null Current.TenantId indicates that we have set a tenant id value explicitly.
     */

    public interface ICurrentTenantAccessor
    {
        BasicTenantInfo Current { get; set; }
    }
}
