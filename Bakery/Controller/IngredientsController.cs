using Bakery.Data;
using Bakery.DTO;
using Bakery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controller;

[Route("[controller]")]
[ApiController]
public class IngredientsController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly ILogger<IngredientsController> _logger;

    public IngredientsController(MyDbContext context, ILogger<IngredientsController>logger)
    {
        _context = context;
        _logger = logger;
    }

    [Authorize(Roles = $"{UserRoles.Baker}, {UserRoles.Administrator}, {UserRoles.Manager}")]
    [HttpGet("Query1")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
    {

        List<int> ingredientIds = new List<int> { 6, 7, 8 };
        
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

    [Authorize(Roles = $"{UserRoles.Baker},{UserRoles.Administrator}, {UserRoles.Manager}")]
    [HttpGet("Query4")]
    public ActionResult<IEnumerable<Batch>> GetIngredientsForBatch(int batchId)
    {
        var query = from i in _context.Ingredients
            join bi in _context.BatchIngredient on i.IngredientId equals bi.IngredientsId
            where batchId.Equals(bi.BatchId)
            select new
            {
                IngredientName = i.IngredientName,
                StockQuantity = i.StockQuantity,
                Allergens = i.Allergens //Grundet D1
            };

        var result = query.ToList();
        return Ok(result);
    }







    [Authorize(Roles = $"{UserRoles.Administrator}")]
    [HttpPost("C1")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> AddIngredientAndQuantity(IngredientDTO ingredientDTO)
    {
        var timestamp = new DateTimeOffset(DateTime.UtcNow);
        var loginfo = new { Operation = "Post added ingredient", Timestamp = timestamp };
        
        _logger.LogInformation("Post called {@loginfo} ", loginfo);
        //Add a new ingredient and quantity to the stock
        if (ingredientDTO.StockQuantity < 0)
        {
            return BadRequest("Ingredient quantity must be non-negative");
        }

        var newIngredient = new Ingredient
        {
            IngredientName = ingredientDTO.name,
            StockQuantity = ingredientDTO.StockQuantity,
            Allergens = ingredientDTO.Allergens
        };

        _context.Ingredients.Add(newIngredient);
        await _context.SaveChangesAsync();

        return Ok(newIngredient);
    }

    [Authorize(Roles = $"{UserRoles.Administrator}")]
    [HttpPut("C2")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> UpdateIngredientStock(int id, IngredientDTO ingredientDTO)
    {
        var timestamp = new DateTimeOffset(DateTime.UtcNow);
        var loginfo = new { Operation = "Put Ingredients stock", Timestamp = timestamp };
        
        _logger.LogInformation("Put called {@loginfo} ", loginfo);
        
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

    [Authorize(Roles = $"{UserRoles.Administrator}")]
    [HttpDelete("C3")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> DeleteIngredient(int id)
    {
        var timestamp = new DateTimeOffset(DateTime.UtcNow);
        var loginfo = new { Operation = "Delete Ingredient", Timestamp = timestamp };
        
        _logger.LogInformation("Delete called {@loginfo} ", loginfo);
        
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