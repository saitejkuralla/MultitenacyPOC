using System.ComponentModel.DataAnnotations;

namespace IMM.Core.API.JWT.Models
{
    public class UserModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
