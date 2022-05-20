using IMM.Core.API.DTO;

namespace IMM.Core.API
{
    public class APITenantInfo
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public List<ProductDTO>? Products { get; set; }
    }
}
