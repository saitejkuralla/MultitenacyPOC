using IMM.MultiTenancy.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy.Security
{

    //TODO _principalAccessor
    public class CurrentUser:ICurrentUser, ITransientDependency
    {
        public virtual bool IsAuthenticated => Id.HasValue;


        //TODO _principalAccessor
        public virtual Guid? Id =>new Guid();
        public virtual Guid? TenantId => new Guid();

        //  public virtual Guid? TenantId => _principalAccessor.Principal?.FindTenantId();
        //  public virtual Guid? Id => _principalAccessor.Principal?.FindUserId();
    }
}
