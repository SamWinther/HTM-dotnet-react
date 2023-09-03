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
    public class RcmtypesController : ControllerBase
    {
        private readonly HtmContext _context;

        public RcmtypesController(HtmContext context)
        {
            _context = context;
        }

        // GET: api/Rcmtypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rcmtype>>> GetRcmtypes()
        {
          if (_context.Rcmtypes == null)
          {
              return NotFound();
          }
            return await _context.Rcmtypes.ToListAsync();
        }

        // GET: api/Rcmtypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rcmtype>> GetRcmtype(int id)
        {
          if (_context.Rcmtypes == null)
          {
              return NotFound();
          }
            var rcmtype = await _context.Rcmtypes.FindAsync(id);

            if (rcmtype == null)
            {
                return NotFound();
            }

            return rcmtype;
        }

        // PUT: api/Rcmtypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRcmtype(int id, Rcmtype rcmtype)
        {
            if (id != rcmtype.Id)
            {
                return BadRequest();
            }

            _context.Entry(rcmtype).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RcmtypeExists(id))
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

        // POST: api/Rcmtypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rcmtype>> PostRcmtype(Rcmtype rcmtype)
        {
          if (_context.Rcmtypes == null)
          {
              return Problem("Entity set 'HtmContext.Rcmtypes'  is null.");
          }
            _context.Rcmtypes.Add(rcmtype);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRcmtype", new { id = rcmtype.Id }, rcmtype);
        }

        // DELETE: api/Rcmtypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRcmtype(int id)
        {
            if (_context.Rcmtypes == null)
            {
                return NotFound();
            }
            var rcmtype = await _context.Rcmtypes.FindAsync(id);
            if (rcmtype == null)
            {
                return NotFound();
            }

            _context.Rcmtypes.Remove(rcmtype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RcmtypeExists(int id)
        {
            return (_context.Rcmtypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        //get related data
        [HttpGet("Alaki")]
        public async Task<ActionResult<Rcmtype>> Alaki(string? searchWord)
        {
            if (_context.Rcmtypes == null)
            {
                return NotFound();
            }
            var rcmtype = await _context.Rcmtypes.FindAsync(2);

            if (rcmtype == null)
            {
                return NotFound();
            }

            return rcmtype;
        }
    }
}
