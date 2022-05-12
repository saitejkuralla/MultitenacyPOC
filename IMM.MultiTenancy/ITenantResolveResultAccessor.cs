using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy
{
    public interface ITenantResolveResultAccessor
    {
        [CanBeNull]
        TenantResolveResult Result { get; set; }
    }
}
