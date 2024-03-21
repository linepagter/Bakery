using Bakery.Data;
using Bakery.DTO;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Controller;

[Route("[controller]")]
[ApiController]
public class IngredientsController: ControllerBase
{
    private readonly MyDbContext _context;

    public IngredientsController(MyDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("Query1")]
        public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
        {

            List<int> ingredientIds = new List<int> { 9,10,11 };

            var query = from i in _context.Ingredients
                where ingredientIds.Contains(i.IngredientId)
                select new
                {
                    IngredientName = i.IngredientName,
                    StockQuantity = i.StockQuantity
                };

            var result = query.ToList();
            return Ok(result);
        }
    
}