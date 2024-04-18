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
    private readonly ILogger<BatchController> _logger;


    public BatchController(MyDbContext context, ILogger<BatchController>logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [Authorize(Roles = $"{UserRoles.Baker}, {UserRoles.Administrator}, {UserRoles.Manager}")]
    [HttpGet("Query7")]
    public ActionResult<double> GetDelay()
    {
        List<int> batchIds = new List<int> { 1,2,3 };

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