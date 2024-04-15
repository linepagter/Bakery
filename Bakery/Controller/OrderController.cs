using Bakery.Data;
using Bakery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Controller;

[Route("[controller]")]
[ApiController]
public class OrderController: ControllerBase
{
    private readonly MyDbContext _context;

    public OrderController(MyDbContext context)
    {
        _context = context;
    }
    
    [Authorize(Roles = "Admin, Driver, Manager")]
    [HttpGet("Query2")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {

        int orderId =  3 ;

        var query = from o in _context.Orders
            where orderId.Equals(o.OrderId)
            select new
            {
                DeliveryAdress = o.DeliveryPlace,
                DeliveryDate = o.DeliveryDate
            };

        var result = query.ToList();
        return Ok(result);
    }
    
}