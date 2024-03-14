using Bakery.Data;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controller;


[Route("[controller]")]
[ApiController]
public class Made_inController: ControllerBase
{
    private readonly MyDbContext _context;

    public Made_inController(MyDbContext context)
    {
        _context = context;
    }
    
}