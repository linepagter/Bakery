using System.Diagnostics;
using Azure;
using Bakery.Data;
using Bakery.DTO;
using Bakery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Controller;


[Route("[controller]")]
[ApiController]
public class BakingGoodsController: ControllerBase
{
    private readonly MyDbContext _context;
    

    public BakingGoodsController(MyDbContext context)
    {
        _context = context;
    }
    
    [Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Driver}, {UserRoles.Manager}")]
    [HttpGet("Query3")]
    public async Task<ActionResult<IEnumerable<BakingGood>>> GetBakedGoods(int orderId)
    {
        var query = from bg in _context.BakingGoods
            join bgo in _context.BakingGoodOrders on bg.BakingGoodId equals bgo.BakingGoodId
            where bgo.OrderId == orderId
            select new
            {
                BakingName = bg.BakingGoodName,
                Quantity = bg.Quantity
            };

        var result = await query.ToListAsync();
        return Ok(result);
    }
    
    [Authorize(Roles = $"{UserRoles.Administrator}, {UserRoles.Manager}")]
    [HttpGet("Query6")]
    public async Task<ActionResult<IEnumerable<BakingGood>>> GetAll()
    {
        var query = from bg in _context.BakingGoods
            group bg by bg.BakingGoodName into g
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