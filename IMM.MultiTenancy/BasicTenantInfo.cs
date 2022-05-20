using IMM.MultiTenancy.Data;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy
{
    public class BasicTenantInfo
    {
        /// <summary>
        /// Null indicates the host.
        /// Not null value for a tenant.
        /// </summary>
        [CanBeNull]
        public Guid? TenantId { get; }

        /// <summary>
        /// Name of the tenant if <see cref="TenantId"/> is not null.
        /// </summary>
        [CanBeNull]
        public string Name { get; }
        public ConnectionStrings ConnectionStrings { get; }

        public BasicTenantInfo(Guid? tenantId, string name = null, ConnectionStrings connectionStrings  = null)
        {
            TenantId = tenantId;
            Name = name;
            ConnectionStrings = connectionStrings;
        }
    }
}
