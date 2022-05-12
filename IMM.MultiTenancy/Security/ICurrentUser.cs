using JetBrains.Annotations;

namespace IMM.MultiTenancy.Security
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }

        [CanBeNull]
        Guid? Id { get; }
        Guid? TenantId { get; }
    }
}