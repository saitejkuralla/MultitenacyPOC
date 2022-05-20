using IMM.MultiTenancy.DependencyInjection;
using IMM.MultiTenancy.Extentions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.Data
{
    public class DefaultConnectionStringResolver : IConnectionStringResolver, ITransientDependency
    {
        protected IMMDbConnectionOptions Options { get; }

        public DefaultConnectionStringResolver(
            IOptionsMonitor<IMMDbConnectionOptions> options)
        {
            Options = options.CurrentValue;
        }

        [Obsolete("Use ResolveAsync method.")]
        public virtual string Resolve(string connectionStringName = null)
        {
            return ResolveInternal(connectionStringName);
        }

        public virtual Task<string> ResolveAsync(string connectionStringName = null)
        {
            return Task.FromResult(ResolveInternal(connectionStringName));
        }

        private string ResolveInternal(string connectionStringName)
        {
            if (connectionStringName == null)
            {
                return Options.ConnectionStrings.Default;
            }

            var connectionString = Options.GetConnectionStringOrNull(connectionStringName);

            if (!connectionString.IsNullOrEmpty())
            {
                return connectionString;
            }

            return null;
        }
    }
}
