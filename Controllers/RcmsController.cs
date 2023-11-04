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
            //return await _context.Rcms.ToListAsync();
            //alternative
            var query = from Rcm in _context.Rcms select new { Rcm };
            //alternative
            //var query = from Rcm in _context.Set<Rcm>() select new {Rcm};
            var list = query.Select(x => x.Rcm).ToList();
            return list;

        }

        // GET: api/Rcms
        [HttpGet("linq")]
        public IQueryable<RcmDTO> GetRcms_linq()
        //public List<RcmDTO> GetRcms_linq()
        {
            if (_context.Rcms == null)
            {
                Console.WriteLine("Not found");
            }
            var query = from Rcm in _context.Set<Rcm>() join Rcmtype in _context.Set<Rcmtype>() on Rcm.Rcmtype equals Rcmtype.Id select new RcmDTO {
                Rcmtype = Rcmtype.Type,
                Rcmtext = Rcm.Rcmtext,
                NewRiskFromRcm = (Rcm.NewRiskFromRcm) ? "Yes": "No",
                Implement = Rcm.Implement,
                VerOfEff = Rcm.VerOfEff
            };
            //var list = query.ToList();
            Console.WriteLine("Ow. it is working.");
            return query;
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

        //GET: api/RCM/RCMwithType/5
        [HttpGet("/RCMwithType/{Id}")]
        public List<HTMbackend.HTM.Rcm> RiskWithRCMs(int riskId)
        {
            var RCMwithType = _context.Rcms.Include(Rcm => Rcm.RcmtypeNavigation).ToList();

            //if (RCMwithType == null)
            //{
            //    throw new Exception("this is riskFind error");
            //}

            return RCMwithType;
        }

        private bool RcmExists(int id)
        {
            return (_context.Rcms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
