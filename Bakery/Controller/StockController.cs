using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controller;

[Route("[controller]")]
[ApiController]
public class StockController: ControllerBase
{
    private readonly MyDbContext _context;

    public StockController(MyDbContext context)
    {
        _context = context;
    }
    
}