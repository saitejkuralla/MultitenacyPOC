using IMM.MultiTenancy.Data;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMM.MultiTenancy
{
    public interface ICurrentTenant
    {
        bool IsAvailable { get; }

        [CanBeNull]
        Guid? Id { get; }

        [CanBeNull]
        string Name { get; }
        [CanBeNull]
        ConnectionStrings connectionStrings { get; }

        IDisposable Change(Guid? id, string name = null, ConnectionStrings connectionStrings=null);
    }
}
