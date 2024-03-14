using Bakery.Data;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Controller;


[Route("[controller]")]
[ApiController]
public class BatchIngredientController: ControllerBase
{
    private readonly MyDbContext _context;

    public BatchIngredientController(MyDbContext context)
    {
        _context = context;
    }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BatchIngredient>>> GetBatchIngredient()
        {
            return await _context.BatchIngredient.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BatchIngredient>> GetBatchIngredient(int id)
        {
            var batchIngredient = await _context.BatchIngredient.FindAsync(id);

            if (batchIngredient == null)
            {
                return NotFound();
            }

            return batchIngredient;
        }

        [HttpPost]
        public async Task<ActionResult<BatchIngredient>> PostBatchIngredient(BatchIngredient batchIngredient)
        {
            _context.BatchIngredient.Add(batchIngredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBatchIngredient", new { id = batchIngredient.IngredientsId }, batchIngredient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBatchIngredient(int id, BatchIngredient batchIngredient)
        {
            if (id != batchIngredient.IngredientsId)
            {
                return BadRequest();
            }

            _context.Entry(batchIngredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatchIngredientExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBatchIngredient(int id)
        {
            var batchIngredient = await _context.BatchIngredient.FindAsync(id);
            if (batchIngredient == null)
            {
                return NotFound();
            }

            _context.BatchIngredient.Remove(batchIngredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BatchIngredientExists(int id)
        {
            return _context.BatchIngredient.Any(e => e.IngredientsId == id);
        }
    
}