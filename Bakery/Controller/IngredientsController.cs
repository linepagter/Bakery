using Bakery.Data;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controller;

[Route("[controller]")]
[ApiController]
public class IngredientsController: ControllerBase
{
    private readonly MyDbContext _context;

    public IngredientsController(MyDbContext context)
    {
        _context = context;
    }
    
}