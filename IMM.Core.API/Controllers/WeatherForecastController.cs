using IMM.Core.API.JWT;
using IMM.Core.API.JWT.Models;
using IMM.Core.API.Repository;
using IMM.MultiTenancy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace IMM.Core.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        private string generatedToken = null;

        private readonly ICurrentTenant _currentTenant;

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration config, ITokenService tokenService, IUserRepository userRepository, ICurrentTenant currentTenant)
        {
            _logger = logger;
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _currentTenant = currentTenant;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public APITenantInfo Get()
        {

            var foreCastData = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            var result = new APITenantInfo()
            {
                Id = _currentTenant.Id.Value,
                Name = _currentTenant.Name,
                WeatherForecast = foreCastData
            };

            return result;
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