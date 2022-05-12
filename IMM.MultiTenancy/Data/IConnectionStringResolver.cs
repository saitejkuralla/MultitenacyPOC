using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.Data
{
    public interface IConnectionStringResolver
    {
        [NotNull]
        [Obsolete("Use ResolveAsync method.")]
        string Resolve(string connectionStringName = null);

        [NotNull]
        Task<string> ResolveAsync(string connectionStringName = null);
    }
}
