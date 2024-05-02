using Bakery.Data;
using Bakery.DTO;
using Bakery.Models;
using Bakery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controller;

[Route("[controller]")]
[ApiController]
public class IngredientsController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly ILogger<IngredientsController> _logger;
    private readonly LogService _logService;


    public IngredientsController(MyDbContext context, ILogger<IngredientsController>logger, LogService logService)
    {
        _context = context;
        _logger = logger;
        _logService = logService;
    }

    [Authorize(Roles = $"{UserRoles.Baker}, {UserRoles.Administrator}, {UserRoles.Manager}")]
    [HttpGet("Query1")]
    public async Task<ActionResult<IEnumerable<Ingredient>>> GetIngredients()
    {


        
        var query = from i in _context.Ingredients
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
    public ActionResult<IEnumerable<Batch>> GetIngredientsForBatch([FromQuery] int batchId)
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
    public async Task<ActionResult<IEnumerable<Ingredient>>> AddIngredientAndQuantity([FromQuery]IngredientDTO ingredientDTO)
    {
        var user = HttpContext.User.Identity.Name;
        var timestamp = new DateTimeOffset(DateTime.Now);
        var Loginfo = new { Operation = "POST", Timestamp = timestamp, User= user };
        
        _logger.LogInformation("added ingredient - {@Loginfo} ", Loginfo);
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
    public async Task<ActionResult<IEnumerable<Ingredient>>> UpdateIngredientStock([FromQuery]int id, [FromQuery]IngredientDTO ingredientDTO)
    {
        var user = HttpContext.User.Identity.Name;
        var timestamp = new DateTimeOffset(DateTime.Now);
        var Loginfo = new { Operation = "PUT", Timestamp = timestamp, User= user };
        
        _logger.LogInformation("Ingredients stock -  {@Loginfo} ", Loginfo);
        
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
    public async Task<ActionResult<IEnumerable<Ingredient>>> DeleteIngredient([FromQuery]int id)
    {
        var user = HttpContext.User.Identity.Name;
        var timestamp = new DateTimeOffset(DateTime.Now);
        var Loginfo = new { Operation = "DELETE", Timestamp = timestamp, User= user };
        
        _logger.LogInformation("Ingredient -  {@Loginfo} ", Loginfo);
        
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