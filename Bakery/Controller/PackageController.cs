using System.Diagnostics;
using Bakery.Data;
using Bakery.DTO;
using Bakery.Models;
using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Driver}, {UserRoles.Manager}")]
    [HttpGet("Query5")]
    public async Task<ActionResult<IEnumerable<Package>>> GetTrackId(int orderId)
    {

        var query = from p in _context.Packages
            join o in _context.Orders on p.OrderId equals o.OrderId //Joiner grundet D3
            where orderId.Equals(p.OrderId)
            select new
            {
                TrackId = p.TrackId,
                Address = o.DeliveryPlace, //Grundet D3
                GPSCoordinates = o.GPSCoordinates //Grundet D3
            };

        var result = query.ToList();
        return Ok(result);
    }

        
    
    
}