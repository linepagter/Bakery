using Bakery.Data;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controller;


[Route("[controller]")]
[ApiController]
public class UsesController: ControllerBase
{
    private readonly MyDbContext _context;

    public UsesController(MyDbContext context)
    {
        _context = context;
    }
    
}