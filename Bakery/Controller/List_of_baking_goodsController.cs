using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controller;


[Route("[controller]")]
[ApiController]
public class List_of_baking_goodsController: ControllerBase
{
    private readonly MyDbContext _context;

    public List_of_baking_goodsController(MyDbContext context)
    {
        _context = context;
    }
    
}