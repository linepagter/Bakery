using Bakery.Data;
using Bakery.DTO;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controller;

[Route("[controller]")]
[ApiController]
public class IngredientsController : ControllerBase
{
    private readonly MyDbContext _context;

    public IngredientsController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet("Query1")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
    {

        List<int> ingredientIds = new List<int> { 9, 10, 11 };

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

    [HttpGet("Query4")]
    public ActionResult<IEnumerable<Batch>> GetIngredientsForBatch(int batchId)
    {
        var ingredients = _context.Batch
            .Where(u => u.BatchId == batchId)  
            .Join(_context.Ingredients,
                u => u.IngredientId, i => i.IngredientId,
                (u, i) => new Batch
                {
                    IngredientName = i.IngredientName,
                    StockQuantity = u.Quantity
                })
            .ToList();

        return Ok(ingredients);
    }








    [HttpPost("C1")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> AddIngredientAndQuantity(IngredientDTO ingredientDTO)
    {
        //Add a new ingredient and quantity to the stock
        if (ingredientDTO.StockQuantity < 0)
        {
            return BadRequest("Ingredient quantity must be non-negative");
        }

        var newIngredient = new Ingredient
        {
            IngredientName = ingredientDTO.name,
            StockQuantity = ingredientDTO.StockQuantity
        };

        _context.Ingredients.Add(newIngredient);
        await _context.SaveChangesAsync();

        return Ok(newIngredient);
    }

    [HttpPut("C2")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> UpdateIngredientStock(int id, IngredientDTO ingredientDTO)
    {
        //Update a quantity of an ingredient in stock
        var ingredient = await _context.Ingredients.FindAsync(id);

        if (ingredientDTO.StockQuantity < 0)
        {
            return BadRequest("Ingredient quantity must be non-negative");
        }

        if (ingredient == null)
        {
            return NotFound();
        }

        ingredient.StockQuantity = ingredientDTO.StockQuantity;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("C3")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> DeleteIngredient(int id)
    {
        //Delete an ingredient from the stock
        var ingredient = await _context.Ingredients.FindAsync(id);

        if (ingredient == null)
        {
            return NotFound();
        }

        _context.Ingredients.Remove(ingredient);

        await _context.SaveChangesAsync();

        return NoContent();
    }



}