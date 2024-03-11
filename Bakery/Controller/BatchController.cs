using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controller;

[Route("[controller]")]
[ApiController]
public class BatchController: ControllerBase
{
    private readonly MyDbContext _context;

    public BatchController(MyDbContext context)
    {
        _context = context;
    }
    
}