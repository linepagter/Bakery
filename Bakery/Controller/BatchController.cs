using Bakery.Data;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Controller;

[Route("[controller]")]
[ApiController]
public class BatchController: ControllerBase
{
    private readonly MyDbContext _context;

    public BatchController(MyDbContext context)
    {
        _context = context;
    }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batch>>> GetBatch()
        {
            return await _context.Batch.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Batch>> GetBatch(int id)
        {
            var batch = await _context.Batch.FindAsync(id);

            if (batch == null)
            {
                return NotFound();
            }

            return batch;
        }

        [HttpPost]
        public async Task<ActionResult<Batch>> PostBatch(Batch batch)
        {
            _context.Batch.Add(batch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBatch", new { id = batch.BatchId }, batch);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatch(int id, Batch batch)
        {
            if (id != batch.BatchId)
            {
                return BadRequest();
            }

            _context.Entry(batch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatchExists(id))
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

        // DELETE: api/Batch/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatch(int id)
        {
            var batch = await _context.Batch.FindAsync(id);
            if (batch == null)
            {
                return NotFound();
            }

            _context.Batch.Remove(batch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BatchExists(int id)
        {
            return _context.Batch.Any(e => e.BatchId == id);
        }
    
}