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
        public async Task<ActionResult<IEnumerable<ListOfBakingGoods>>> GetListOfBakingGoods()
        {
            return await _context.ListOfBakingGoods.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListOfBakingGoods>> GetListOfBakingGoods(int id)
        {
            var listOfBakingGoods = await _context.ListOfBakingGoods.FindAsync(id);

            if (listOfBakingGoods == null)
            {
                return NotFound();
            }

            return listOfBakingGoods;
        }

        [HttpPost]
        public async Task<ActionResult<ListOfBakingGoods>> PostListOfBakingGoods(ListOfBakingGoods listOfBakingGoods)
        {
            _context.ListOfBakingGoods.Add(listOfBakingGoods);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListOfBakingGoods", new { id = listOfBakingGoods.ListId }, listOfBakingGoods);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutListOfBakingGoods(int id, ListOfBakingGoods listOfBakingGoods)
        {
            if (id != listOfBakingGoods.ListId)
            {
                return BadRequest();
            }

            _context.Entry(listOfBakingGoods).State = EntityState.Modified;

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
            var listOfBakingGoods = await _context.ListOfBakingGoods.FindAsync(id);
            if (listOfBakingGoods == null)
            {
                return NotFound();
            }

            _context.ListOfBakingGoods.Remove(listOfBakingGoods);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListOfBakingGoodsExists(int id)
        {
            return _context.ListOfBakingGoods.Any(e => e.ListId == id);
        }
    
    
}