using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMM.MultiTenancy.Data;
using IMM.MultiTenancy.Extentions;
using JetBrains.Annotations;

namespace IMM.MultiTenancy
{
    public class TenantConfiguration
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ConnectionStrings ConnectionStrings { get; set; }

        public bool IsActive { get; set; }

        public TenantConfiguration()
        {
            IsActive = true;
        }

        public TenantConfiguration(Guid id, [NotNull] string name,[NotNull] ConnectionStrings connectionStrings)
            : this()
        {
            Check.NotNull(name, nameof(name));

            Id = id;
            Name = name;

            ConnectionStrings = connectionStrings;
        }
    }
}
