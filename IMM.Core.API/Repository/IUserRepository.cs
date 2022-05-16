using IMM.Core.API.JWT.Models;

namespace IMM.Core.API.Repository
{
    public interface IUserRepository
    {
        UserDTO GetUser(UserModel userModel);
    }
}
