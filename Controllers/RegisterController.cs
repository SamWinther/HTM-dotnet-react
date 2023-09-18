using HTMbackend.HTM;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HTMbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly HtmContext _context;
        public RegisterController(HtmContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser(Risk risk)
        {
            if (_context.Risks == null)
            {
                return Problem("Entity set 'HtmContext.Risks'  is null.");
            }
            _context.Risks.Add(risk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRisk", new { id = risk.Id }, risk);
        }

    }
}
