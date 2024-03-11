using Bakery.Data;
using Bakery.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Bakery.Controller;

[Route("[controller]")]
[ApiController]
public class BakeryController:ControllerBase
{
    private readonly MyDbContext _context;

    private readonly ILogger<BakeryController> _logger;

    public BakeryController(
        MyDbContext context,
        ILogger<BakeryController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet(Name = "GetBakery")]
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
    public async Task<RestDTO<BoardGame[]>> Get(
        [FromQuery] RequestDTO<BoardGameDTO> input)
    {
        var query = _context.BoardGames.AsQueryable();
        if (!string.IsNullOrEmpty(input.FilterQuery))
            query = query.Where(b => b.Name.Contains(input.FilterQuery));
        var recordCount = await query.CountAsync();
        query = query
            .OrderBy($"{input.SortColumn} {input.SortOrder}")
            .Skip(input.PageIndex * input.PageSize)
            .Take(input.PageSize);
    
        return new RestDTO<BoardGame[]>()
        {
            Data = await query.ToArrayAsync(),
            PageIndex = input.PageIndex,
            PageSize = input.PageSize,
            RecordCount = recordCount,
            Links = new List<LinkDTO> {
                new LinkDTO(
                    Url.Action(
                        null,
                        "BoardGames",
                        new { input.PageIndex, input.PageSize },
                        Request.Scheme)!,
                    "self",
                    "GET"),
            }
        };
    }
}