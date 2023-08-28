using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTMbackend.HTM;
using System.Configuration;

namespace HTMbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Rcm2riskController : ControllerBase
    {
        private readonly HtmContext _context;

        public Rcm2riskController(HtmContext context)
        {
            _context = context;
        }

        // GET: api/Rcm2risk
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rcm2risk>>> GetRcm2risks()
        {
            if (_context.Rcm2risks == null)
            {
              return NotFound();
            }
            return await _context.Rcm2risks.ToListAsync();
        }

        // GET: api/Rcm2risk/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rcm2risk>> GetRcm2risk(int id)
        {
          if (_context.Rcm2risks == null)
          {
              return NotFound();
          }
            var rcm2risk = await _context.Rcm2risks.FindAsync(id);

            if (rcm2risk == null)
            {
                return NotFound();
            }

            return rcm2risk;
        }

        // PUT: api/Rcm2risk/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRcm2risk(int id, Rcm2risk rcm2risk)
        {
            if (id != rcm2risk.Id)
            {
                return BadRequest();
            }

            _context.Entry(rcm2risk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Rcm2riskExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Rcm2risk
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rcm2risk>> PostRcm2risk(Rcm2risk rcm2risk)
        {
          if (_context.Rcm2risks == null)
          {
              return Problem("Entity set 'HtmContext.Rcm2risks'  is null.");
          }
            _context.Rcm2risks.Add(rcm2risk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRcm2risk", new { id = rcm2risk.Id }, rcm2risk);
        }

        // DELETE: api/Rcm2risk/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRcm2risk(int id)
        {
            if (_context.Rcm2risks == null)
            {
                return NotFound();
            }
            var rcm2risk = await _context.Rcm2risks.FindAsync(id);
            if (rcm2risk == null)
            {
                return NotFound();
            }

            _context.Rcm2risks.Remove(rcm2risk);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Rcm2riskExists(int id)
        {
            return (_context.Rcm2risks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
