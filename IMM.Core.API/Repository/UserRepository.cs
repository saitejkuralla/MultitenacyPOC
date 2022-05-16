using IMM.Core.API.JWT.Models;

namespace IMM.Core.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<UserDTO> users = new List<UserDTO>();
        public UserRepository()
        {
            users.Add(new UserDTO { UserName = "saitej", Password = "test@123", Role = "manager",TenantID=new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00") });
            users.Add(new UserDTO { UserName = "michaelsanders", Password = "michael321", Role = "developer" ,TenantID = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00") });
            users.Add(new UserDTO { UserName = "stephensmith", Password = "stephen123", Role = "tester", TenantID = new Guid("11223344-5566-7788-99AA-BBCCDDEEFF00") });
            users.Add(new UserDTO { UserName = "rodpaddock", Password = "rod123", Role = "admin" , TenantID = new Guid("22223344-5566-7788-99AA-CCCCDDEEFF11") });
            users.Add(new UserDTO { UserName = "rexwills", Password = "rex321", Role = "admin", TenantID = new Guid("22223344-5566-7788-99AA-CCCCDDEEFF11") });
        }
        public UserDTO GetUser(UserModel userModel)
        {
            return users.Where(x => x.UserName.ToLower() == userModel.UserName.ToLower()
                && x.Password == userModel.Password).FirstOrDefault();
        }
    }
}
