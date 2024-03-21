using System.Diagnostics;
using Bakery.Data;
using Bakery.DTO;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Controller;


[Route("[controller]")]
[ApiController]
public class PackageController: ControllerBase
{
    private readonly MyDbContext _context;

    public PackageController(MyDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("Query5")]
    public async Task<ActionResult<IEnumerable<Package>>> GetTrackId()
    {

        int OrdreID = 11;

        var query = from p in _context.Packages
            where OrdreID.Equals(p.OrderId)
            select new
            {
                TrackId = p.TrackId,
            };

        var result = query.ToList();
        return Ok(result);
    }

        
    
    
}