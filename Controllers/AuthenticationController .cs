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

namespace HTMbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly HtmContext _context;

        public AuthenticationController(HtmContext context)
        {
            _context = context;
        }

        public HttpRequest GetRequest1()
        {
            return Request;
        }

        // GET: api/authentication/username
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
    }
}
