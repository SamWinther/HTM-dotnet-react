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
    public class Rcm2risksController : ControllerBase
    {
        private readonly HtmContext _context;

        public Rcm2risksController(HtmContext context)
        {
            _context = context;
        }

        // GET: api/Rcm2risks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rcm2risk>>> GetRcm2risks()
        {
          if (_context.Rcm2risks == null)
          {
              return NotFound();
          }
            return await _context.Rcm2risks.ToListAsync();
        }

        // GET: api/RcmsOfRisk/5
        [HttpGet("GetRcmsofRisk/{id}")]
        public List<HTMbackend.HTM.Rcm2risk> GetRcmsofRisk(int id)
        {
          //if (_context.Rcm2risks == null)
          //{
          //    Exception;
          //}
            var rcmIds = _context.Rcm2risks.Where(r => r.RiskId == id).ToList();

            //if (rcmIds == null)
            //{
            //    return NotFound();
            //}

            return rcmIds;
        }

        // GET: api/RisksOfRCM/5
        [HttpGet("GetRisksOfRCM/{id}")]
        public List<HTMbackend.HTM.Rcm2risk> GetRisksOfRCM(int id)
        {
            //if (_context.Rcm2risks == null)
            //{
            //    Exception;
            //}
            var riskIds = _context.Rcm2risks.Where(r => r.RcmId == id).ToList();

            //if (rcmIds == null)
            //{
            //    return NotFound();
            //}

            return riskIds;
        }

        // POST: api/Rcm2risks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rcm2risk>> PostRcm2risk(Rcm2risk Rcm2risk)
        {
            Console.WriteLine("Hello from post RCM2risk");
          if (_context.Rcm2risks == null)
          {
              return Problem("Entity set 'HtmContext.Rcm2risks'  is null.");
          }
            _context.Rcm2risks.Add(Rcm2risk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRcm2risks", new { id = Rcm2risk.Id }, Rcm2risk);
        }

        // DELETE: api/Rcm2risks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRcm2risk(int id)
        {
            if (_context.Rcm2risks == null)
            {
                return NotFound();
            }
            var Rcm2risk = await _context.Rcm2risks.FindAsync(id);
            if (Rcm2risk == null)
            {
                return NotFound();
            }

            _context.Rcm2risks.Remove(Rcm2risk);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool Rcm2riskExists(Rcm2risk Rcm2risk)
        {
            return (_context.Rcm2risks?.Any(e => (e.RiskId == Rcm2risk.RiskId && e.RcmId == Rcm2risk.RcmId))).GetValueOrDefault();
        }
    }
}
