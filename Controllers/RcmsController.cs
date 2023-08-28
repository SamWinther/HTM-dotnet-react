using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTMbackend.HTM;

namespace HTMbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RcmsController : ControllerBase
    {
        private readonly HtmContext _context;

        public RcmsController(HtmContext context)
        {
            _context = context;
        }

        // GET: api/Rcms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rcm>>> GetRcms()
        {
          if (_context.Rcms == null)
          {
              return NotFound();
          }
            return await _context.Rcms.ToListAsync();
        }

        // GET: api/Rcms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rcm>> GetRcm(int id)
        {
          if (_context.Rcms == null)
          {
              return NotFound();
          }
            var rcm = await _context.Rcms.FindAsync(id);

            if (rcm == null)
            {
                return NotFound();
            }

            return rcm;
        }

        // PUT: api/Rcms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRcm(int id, Rcm rcm)
        {
            if (id != rcm.Id)
            {
                return BadRequest();
            }

            _context.Entry(rcm).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RcmExists(id))
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

        // POST: api/Rcms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rcm>> PostRcm(Rcm rcm)
        {
          if (_context.Rcms == null)
          {
              return Problem("Entity set 'HtmContext.Rcms'  is null.");
          }
            _context.Rcms.Add(rcm);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRcm", new { id = rcm.Id }, rcm);
        }

        // DELETE: api/Rcms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRcm(int id)
        {
            if (_context.Rcms == null)
            {
                return NotFound();
            }
            var rcm = await _context.Rcms.FindAsync(id);
            if (rcm == null)
            {
                return NotFound();
            }

            _context.Rcms.Remove(rcm);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RcmExists(int id)
        {
            return (_context.Rcms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
