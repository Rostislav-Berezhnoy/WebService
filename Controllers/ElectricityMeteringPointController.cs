using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace WebService.Controllers
{
    [Route("api/ElectricityMeteringPoint")]
    [ApiController]
    public class ElectricityMeteringPointController : ControllerBase
    {
        private readonly AppTestContext _context;

        public ElectricityMeteringPointController(AppTestContext context)
        {
            _context = context;
        }

        // GET: api/ElectricityMeteringPoint
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElectricityMeteringPoint>>> GetElectricityMeteringPoint()
        {
            var items = _context.ElectricityMeteringPoint;
            foreach (var item in items)
            {
                item.ConsumptionObjectName = _context.ConsumptionObject.Where(c => c.ID == item.ConsumptionObjectID).FirstOrDefault().Name;
            }
            
            return await items.ToListAsync();
        }

        // GET: api/ElectricityMeteringPoint/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ElectricityMeteringPoint>> GetElectricityMeteringPoint(int id)
        {
            var electricityMeteringPoint = await _context.ElectricityMeteringPoint
                .Include(b => b.ElectricalTransformer)
                .Include(b => b.ElectricityMeter)
                .Include(b => b.VoltageTransformer)
                .FirstOrDefaultAsync(i => i.ID == id);

            if (electricityMeteringPoint == null)
            {
                return NotFound();
            }

            return electricityMeteringPoint;
        }

        // PUT: api/ElectricityMeteringPoint/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutElectricityMeteringPoint(int id, ElectricityMeteringPoint electricityMeteringPoint)
        {
            if (id != electricityMeteringPoint.ID)
            {
                return BadRequest();
            }

            _context.Entry(electricityMeteringPoint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectricityMeteringPointExists(id))
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

        // POST: api/ElectricityMeteringPoint
        [HttpPost]
        public async Task<ActionResult<ElectricityMeteringPoint>> PostElectricityMeteringPoint(ElectricityMeteringPoint electricityMeteringPoint)
        {
        
            _context.ElectricityMeteringPoint.Add(electricityMeteringPoint);

            var VoltageTransformer = electricityMeteringPoint.VoltageTransformer.First();
            VoltageTransformer.ElectricityMeteringPointID = electricityMeteringPoint.ID;

            var ElectricalTransformer = electricityMeteringPoint.ElectricalTransformer.First();
            ElectricalTransformer.ElectricityMeteringPointID = electricityMeteringPoint.ID;

            var ElectricityMeter = electricityMeteringPoint.ElectricityMeter.First();
            ElectricityMeter.ElectricityMeteringPointID = electricityMeteringPoint.ID;

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetElectricityMeteringPoint), new { id = electricityMeteringPoint.ID}, electricityMeteringPoint);
        }

        // DELETE: api/ElectricityMeteringPoint/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElectricityMeteringPoint(int id)
        {
            var electricityMeteringPoint = await _context.ElectricityMeteringPoint.FindAsync(id);
            if (electricityMeteringPoint == null)
            {
                return NotFound();
            }

            _context.ElectricityMeteringPoint.Remove(electricityMeteringPoint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElectricityMeteringPointExists(int id)
        {
            return _context.ElectricityMeteringPoint.Any(e => e.ID == id);
        }
    }
}
