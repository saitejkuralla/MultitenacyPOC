using IMM.MultiTenancy.Data;
using IMM.MultiTenancy.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy
{
    public class CurrentTenant : ICurrentTenant, ITransientDependency
    {
        public virtual bool IsAvailable => Id.HasValue;

        public virtual Guid? Id => _currentTenantAccessor.Current?.TenantId;

        public string Name => _currentTenantAccessor.Current?.Name;

        public ConnectionStrings connectionStrings => _currentTenantAccessor.Current.ConnectionStrings;

        private readonly ICurrentTenantAccessor _currentTenantAccessor;

        public CurrentTenant(ICurrentTenantAccessor currentTenantAccessor)
        {
            _currentTenantAccessor = currentTenantAccessor;
        }

        public IDisposable Change(Guid? id, string name = null, ConnectionStrings connectionStrings = null)
        {
            return SetCurrent(id, name,connectionStrings);
        }

        private IDisposable SetCurrent(Guid? tenantId, string name = null, ConnectionStrings connectionStrings=null)
        {
            var parentScope = _currentTenantAccessor.Current;
            _currentTenantAccessor.Current = new BasicTenantInfo(tenantId, name,connectionStrings);
            return new DisposeAction(() =>
            {
                _currentTenantAccessor.Current = parentScope;
            });
        }
    }
}
