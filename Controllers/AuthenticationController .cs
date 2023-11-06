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
using Newtonsoft.Json;

namespace HTMbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly HtmContext _context;
        //private readonly IConfiguration _configuration;

        public AuthenticationController(HtmContext context)
        {
            _context = context;
        }

        //public AuthenticationController(HtmContext context, IConfiguration configuration)
        //{
        //    _context = context;
        //    _configuration = configuration;
        //}

        // GET: api/authentication/username
        [HttpGet]
        public string AuthenticationWithToken()
        {
            IActionResult response = Unauthorized();
            Console.WriteLine("Backend AuthenticationWithToken is called.");
            if (Request.Headers.ContainsKey("username") && Request.Headers.ContainsKey("password"))
            {
                var getUsers = _context.Users.Include(user => user.Organization).Where(u => u.Username == Request.Headers["username"]).ToList();
                if (getUsers.Count == 0)
                {
                    Console.WriteLine("No user with this username.");
                    string resp = "{\"explain\":\"No user with this username.\"}";
                    return (resp);
                }
                else if (getUsers.Count == 1)
                {
                    User user = getUsers[0];
                    if (user.Username == Request.Headers["username"] && user.Password == Request.Headers["password"])
                    {
                        Console.WriteLine("Login successfull.");

                        UserWithRoles userinfo = getUserInfo(user);

                        var jwtToken = GenerateToken(userinfo);
                        //string resp = "{\"explain\":\"Login successfull.\",\"token\":\""+ jwtToken+ "\",\"Project\":\""+userinfo.Roles[0].Project+"\"}";
                        //string loginSuccessfull = "\"explain\":\"Login successfull.\"";
                        
                        string resp = "{\"explain\":\"Login successfull.\",\"token\":\""+ jwtToken+"\"}";
                        return (resp);
                    }
                    else
                    {
                        Console.WriteLine("Wrong password.");
                        string resp = "{\"explain\":\"Wrong password.\"}";
                        return (resp);
                    }
                }
                else
                {
                    Console.WriteLine("More than one user with this username.");
                    string resp = "{\"explain\":\"More than one user with this username.\"}";
                    return (resp);
                }
            }
            else
            {
                Console.WriteLine("Bad request. The get request HHeader must contain username and password");
                string resp = "{\"explain\":\"Unsuccessful.Header is missing the Username and Password\"}";
                return (resp);
            }
        }

        private string GenerateToken(UserWithRoles user)
        {
            Console.WriteLine("GenerateToken start");
            

            //start to generate the JWT Token
            var issuer = Environment.GetEnvironmentVariable("JwtIssuer");
            var audience = Environment.GetEnvironmentVariable("JwtAudience");
            var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JwtKey"));

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            );


            var subject = new ClaimsIdentity(new[]
            {
                //Hide the information in the token
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Prn, JsonConvert.SerializeObject(user.Roles)),
                new Claim("Organization", user.Organization)
            });

            var expires = DateTime.UtcNow.AddMinutes(10);

            

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = subject,
                Expires = expires,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = signingCredentials,
                //AdditionalHeaderClaims = claims
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            
            Console.WriteLine("loginDataGenerator end");

            return jwtToken;

        }

        private UserWithRoles getUserInfo(User user)
        {
            UserWithRoles userInfo = new UserWithRoles();
            userInfo.Id = user.Id;
            userInfo.FirstName = user.FirstName;
            userInfo.LastName = user.LastName;
            userInfo.Username = user.Username;
            userInfo.Organization = user.Organization.Name;

            var thisOrganization = _context.Organizations.Where(Organization=> Organization.Id == user.OrganizationId).ToList();
            var thisOrganizationProjects = _context.Projects.Where(Project => Project.OrganizationId== user.OrganizationId).ToList();
            var thisUserRoles = _context.UserRoles.Where(UserRole => UserRole.UserID == user.Id).ToList();
            List<string> roleNames = new List<string>();
            foreach (var role in thisUserRoles)
            {
                ProjectRole sampleRole = new ProjectRole();
                sampleRole.Project = role.Project.Name;
                sampleRole.Role = role.EnumRole.ToString();
                sampleRole.Projectid = role.Project.Id;

                userInfo.Roles.Add(sampleRole);
            }
            return userInfo;
        }

        private string convertRoles2String(List<ProjectRole> roles)
        {
            string response = String.Empty;
            foreach (var role in roles)
            {
                response = response + $"{{\"Project\":{role.Project},Role:{role.Role}}},";
            }
            int responseLength = response.Length;
            response = response.Substring(0,responseLength-1);
            response = "["+response+"]";
            return response;
        }
    }
}
