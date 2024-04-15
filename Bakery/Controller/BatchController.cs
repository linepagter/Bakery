using Bakery.Data;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

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
    
    [Authorize(Roles = "Baker, Admin, Manager")]
    [HttpGet("Query7")]
    public ActionResult<double> GetDelay()
    {
        List<int> batchIds = new List<int> { 4, 5, 6 };

        var query = from b in _context.Batch
            where batchIds.Contains(b.BatchId)
            select new
            {
                Delay = (b.FinishTime - b.TargetFinishTime).TotalMinutes
            };

        var average = query.AsEnumerable().Average(b => b.Delay);
        
        return Ok(average);
    }
    
}