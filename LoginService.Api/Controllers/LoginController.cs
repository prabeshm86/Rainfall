using System.Net.Http;
using LoginService.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LoginService.Api.Controllers
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        public ActionResult Login([FromBody]User user)
        {
            User u = new UserRepository().GetUser(user.Username);
            if (u == null)
                return NotFound("The user was not found.");

            bool credentials = u.Password.Equals(user.Password);

            if (!credentials) return Forbid("The username/password combination was wrong.");

            return Ok(TokenManager.GenerateToken(user.Username, _config));
        }


        [HttpGet]
        public ActionResult Validate(string token, string username)
        {
            bool exists = new UserRepository().GetUser(username) != null;
            if (!exists) return NotFound("The user was not found.");
            string tokenUsername = TokenManager.ValidateToken(token, _config);
            if (username.Equals(tokenUsername))
                return Ok();
            return BadRequest();
        }
    }
}
