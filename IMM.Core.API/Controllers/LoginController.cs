using IMM.Core.API.DTO;
using IMM.Core.API.JWT;
using IMM.Core.API.JWT.Models;
using IMM.Core.API.Repository;
using IMM.EntityFrameworkCore.SQL;
using IMM.EntityFrameworkCore.SQL.Data;
using IMM.MultiTenancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace IMM.Core.API.Controllers
{



    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private string generatedToken = null;

        private readonly ApplicationDbContext _context;

        private readonly ICurrentTenant _currentTenant;



        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger, IConfiguration config,
            ITokenService tokenService, IUserRepository userRepository, ICurrentTenant currentTenant, ApplicationDbContext context)
        {
            _logger = logger;
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _currentTenant = currentTenant;
            _context = context;
        }



        [AllowAnonymous]
        [Route("login")]
        [HttpPost]
        public string Login(UserModel userModel)
        {
            if (string.IsNullOrEmpty(userModel.UserName) || string.IsNullOrEmpty(userModel.Password))
            {
                return String.Empty;
            }

            IActionResult response = Unauthorized();
            var validUser = GetUser(userModel);

            if (validUser != null)
            {
                generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(),
                validUser);

                return generatedToken;
            }
            else
            {
                return String.Empty;
            }
        }

        private UserDTO GetUser(UserModel userModel)
        {
            //Write your code here to authenticate the user
            return _userRepository.GetUser(userModel);
        }

        private string BuildMessage(string stringToSplit, int chunkSize)
        {
            var data = Enumerable.Range(0, stringToSplit.Length / chunkSize)
                .Select(i => stringToSplit.Substring(i * chunkSize, chunkSize));

            string result = "The generated token is:";

            foreach (string str in data)
            {
                result += Environment.NewLine + str;
            }

            return result;
        }
    }
}
