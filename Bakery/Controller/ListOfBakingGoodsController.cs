using System.Diagnostics;
using Bakery.Attributes;
using Bakery.Data;
using Bakery.DTO;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Controller;


[Route("[controller]")]
[ApiController]
public class ListOfBakingGoodsController: ControllerBase
{
    private readonly MyDbContext _context;

    public ListOfBakingGoodsController(MyDbContext context)
    {
        _context = context;
    }
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BakingGood>>> GetListOfBakingGoods()
        {
            return await _context.BakingGoods.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BakingGood>> GetListOfBakingGoods(int id)
        {
            var listOfBakingGoods = await _context.BakingGoods.FindAsync(id);

            if (listOfBakingGoods == null)
            {
                return NotFound();
            }

            return listOfBakingGoods;
        }

        [HttpPost]
        public async Task<ActionResult<BakingGood>> PostListOfBakingGoods(BakingGood bakingGood)
        {
            _context.BakingGoods.Add(bakingGood);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListOfBakingGoods", new { id = bakingGood.BakingGoodId }, bakingGood);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutListOfBakingGoods(int id, BakingGood bakingGood)
        {
            if (id != bakingGood.BakingGoodId)
            {
                return BadRequest();
            }

            _context.Entry(bakingGood).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListOfBakingGoodsExists(id))
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
        public async Task<IActionResult> DeleteListOfBakingGoods(int id)
        {
            var listOfBakingGoods = await _context.BakingGoods.FindAsync(id);
            if (listOfBakingGoods == null)
            {
                return NotFound();
            }

            _context.BakingGoods.Remove(listOfBakingGoods);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListOfBakingGoodsExists(int id)
        {
            return _context.BakingGoods.Any(e => e.BakingGoodId == id);
        }
    
    
}