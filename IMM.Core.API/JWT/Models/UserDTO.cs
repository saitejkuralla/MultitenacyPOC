namespace IMM.Core.API.JWT.Models
{
    public class UserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Guid TenantID { get; set; }
    }
}
