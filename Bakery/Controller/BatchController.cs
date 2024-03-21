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
    
    [HttpGet("Query7")]
    public async Task<ActionResult<double>> GetDelay()
    {
        List<int> batchIds = new List<int> { 4,5,6 };

        var query = from b in _context.Batch
            where batchIds.Contains(b.BatchId)
            select new
            {
                Delay = (b.FinishTime - b.TargetFinishTime).TotalMinutes
            };

        var averageDelay = await query.AverageAsync(b => b.Delay);
        
        return Ok(averageDelay);
    }
    
}