using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI_JWT_Sample.JWT.Model;

namespace WebAPI_JWT_Sample.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : Controller
    {

        private readonly TokenSettings _tokenSettings;

        public AuthController(IOptions<TokenSettings> tokenSettings)
        {
            _tokenSettings = tokenSettings.Value ?? throw new ArgumentNullException(nameof(tokenSettings));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("logar")]
        public IActionResult Login(/*LoginUserViemModel loginUser*/)
        {
            //Authentication logic. if success

            var myToken = GerarJwt("");


            return Ok(myToken);
        
        }



        /// <summary>
        /// Method to create a JWT using email of the user (in this case, the email is the user identifier)  
        /// </summary>
        /// <param name="email">User identifier</param>
        /// <returns></returns>
        private string GerarJwt(string email)
        {

            //Get key from appSettings
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);

            //Creating Token Descriptor
            var tokenDescriptor = new SecurityTokenDescriptor {
                Issuer = _tokenSettings.Issuer,
                Audience = _tokenSettings.ValidUrl,
                Expires = DateTime.UtcNow.AddHours(_tokenSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //Creating token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            //Creating token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;

        }


    } //class




}
