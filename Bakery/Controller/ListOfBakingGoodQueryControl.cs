using Bakery.Data;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Controller;

[Route("[controller]")]
[ApiController]


public class ListOfBakingGoodQueryControl:ControllerBase
{
    private readonly MyDbContext _context;

    public ListOfBakingGoodQueryControl(MyDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("Query6")]
    public async Task<ActionResult<IEnumerable<BakingGood>>> GetAll()
    {
        var query = from bg in _context.BakingGoods
            group bg by bg.Type into g
            orderby g.Key ascending
            select new
            {
                BakingName = g.Key,
                TotalQuantity = g.Sum(bg => bg.Quantity)
            };

        var result = await query.ToListAsync();
        return Ok(result);
    }



    

}