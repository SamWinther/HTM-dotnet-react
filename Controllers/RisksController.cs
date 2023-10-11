using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HTMbackend.HTM;
using Microsoft.AspNetCore.Authorization;

namespace HTMbackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RisksController : ControllerBase
    {
        private readonly HtmContext _context;

        public RisksController(HtmContext context)
        {
            _context = context;
        }

        // GET: api/Risks
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Risk>>> GetRisks()
        //public async Task<ActionResult<Risk>> GetRisk(int id)
        {
            if (_context.Risks == null)
          {
              return NotFound();
          }
            return await _context.Risks.ToListAsync();
        }

        // GET: api/Risks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Risk>> GetRisk(int id)
        {
          if (_context.Risks == null)
          {
              return NotFound();
          }
            var risk = await _context.Risks.FindAsync(id);

            if (risk == null)
            {
                return NotFound();
            }

            return risk;
        }

        // PUT: api/Risks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRisk(int id, Risk risk)
        {
            Console.WriteLine("Hello from PUTTTTT");
            if (id != risk.Id)
            {
                return BadRequest();
            }

            _context.Entry(risk).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiskExists(id))
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

        // POST: api/Risks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Risk>> PostRisk(Risk risk)
        {
          if (_context.Risks == null)
          {
              return Problem("Entity set 'HtmContext.Risks'  is null.");
          }
            _context.Risks.Add(risk);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRisk", new { id = risk.Id }, risk);
        }

        // DELETE: api/Risks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRisk(int id)
        {
            if (_context.Risks == null)
            {
                return NotFound();
            }
            var risk = await _context.Risks.FindAsync(id);
            if (risk == null)
            {
                return NotFound();
            }

            _context.Risks.Remove(risk);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/RisksFind/keyword
        [HttpGet("findinScenario/{keyword}")]
        public List<HTMbackend.HTM.Risk> FindinScenario(string keyword)
        {
            var riskWithWord2 = _context.Risks.Where(r=>r.Scenario.Contains(keyword)).ToList();

            //if (riskWithWord2 == null)
            //{
            //    throw new Exception("this is riskFind error");
            //}

            return riskWithWord2;
        }

        // GET: api/Risks/riskwithRCM/5
        [HttpGet("/riskWithRCMs/{riskId}")]
        public List<HTMbackend.HTM.Risk> RiskWithRCMs(int riskId)
        {
            var riskWithWord2 = _context.Risks.Include(risk => risk.Rcm2risks).ToList();

            //if (riskWithWord2 == null)
            //{
            //    throw new Exception("this is riskFind error");
            //}

            return riskWithWord2;
        }

        private bool RiskExists(int id)
        {
            return (_context.Risks?.Any(e => e.Id == id)).GetValueOrDefault();
        }        
    }
}
