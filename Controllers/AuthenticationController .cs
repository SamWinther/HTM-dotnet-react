using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTMbackend.HTM;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace HTMbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly HtmContext _context;
        private readonly IConfiguration _configuration;


        //public AuthenticationController(HtmContext context)
        //{
        //    _context = context;
        //}

        public AuthenticationController(HtmContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //public HttpRequest GetRequest1()
        //{
        //    return Request;
        //}

        // GET: api/authentication/
        [HttpGet]
        public string Authentication()
        {
            Console.WriteLine("Backend Authentication is called.");
            if(Request.Headers.ContainsKey("username") && Request.Headers.ContainsKey("password"))
            {
                Console.WriteLine("USERNAME");
                Console.WriteLine(Request.Headers["username"]);
                Console.WriteLine("PASSWORD");
                Console.WriteLine(Request.Headers["password"]);
                var getUsers = _context.Users.Where(u => u.Username == Request.Headers["username"]).ToList();
                if (getUsers.Count == 0)
                {
                    Console.WriteLine("No user with this username.");
                    string resp = "{\"explain\":\"No user with this username.\",\"data\":[\"null\"]}";

                    return (resp);
                } else if (getUsers.Count == 1)
                {
                    if (getUsers[0].Username == Request.Headers["username"] && getUsers[0].Password == Request.Headers["password"])
                    {
                        Console.WriteLine("Login successfull.");


                        string resp = "{\"explain\":\"Login successfull.\",\"data\":[\"Thi5I5Y0urT0k3n\"]}";

                        return (resp);
                    } else
                    {
                        Console.WriteLine("Wrong password.");
                        string resp = "{\"explain\":\"Wrong password.\",\"data\":[\"null\"]}";

                        return (resp);
                    }
                } else
                {
                    Console.WriteLine("More than one user with this username.");
                    string resp = "{\"explain\":\"More than one user with this username.\",\"data\":[\"null\"]}";

                    return (resp);
                }
            } else
            {
                Console.WriteLine("Bad request. The get request header must contain username and password");
                string resp = "{\"explain\":\"Unsuccessful.Header is missing the Username and Password\",\"data\":[\"null\"]}";
                
                return (resp);
            }
        }

        // GET: api/authentication/username
        [HttpGet("token")]
        public IActionResult AuthenticationWithToken()
        {
            IActionResult response = Unauthorized();
            Console.WriteLine("Backend AuthenticationWithToken is called.");
            if (Request.Headers.ContainsKey("username") && Request.Headers.ContainsKey("password"))
            {
                var getUsers = _context.Users.Where(u => u.Username == Request.Headers["username"]).ToList();
                if (getUsers.Count == 0)
                {
                    Console.WriteLine("No user with this username.");
                    
                    return (response);
                }
                else if (getUsers.Count == 1)
                {
                    User user = getUsers[0];
                    if (user.Username == Request.Headers["username"] && user.Password == Request.Headers["password"])
                    {
                        Console.WriteLine("Login successfull.");

                        var issuer = _configuration["Jwt:Issuer"];
                        var audience = _configuration["Jwt:Audience"];
                        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

                        var signingCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha512Signature
                        );

                        var subject = new ClaimsIdentity(new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                            new Claim(JwtRegisteredClaimNames.Email, user.Username),
                        });

                        var expires = DateTime.UtcNow.AddMinutes(10);

                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = subject,
                            Expires = expires,
                            Issuer = issuer,
                            Audience = audience,
                            SigningCredentials = signingCredentials
                        };

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        var jwtToken = tokenHandler.WriteToken(token);

                        return Ok(jwtToken);
                    }
                    else
                    {
                        Console.WriteLine("Wrong password.");
                        return (response);
                    }
                }
                else
                {
                    Console.WriteLine("More than one user with this username.");
                    return (response);
                }
            }
            else
            {
                Console.WriteLine("Bad request. The get request HHeader must contain username and password");
                return (response);
            }
        }
    }
}
