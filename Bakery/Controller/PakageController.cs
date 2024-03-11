using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controller;


[Route("[controller]")]
[ApiController]
public class PakageController: ControllerBase
{
    private readonly MyDbContext _context;

    public PakageController(MyDbContext context)
    {
        _context = context;
    }
    
}