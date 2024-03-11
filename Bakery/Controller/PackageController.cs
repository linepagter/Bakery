using System.Diagnostics;
using Bakery.Attributes;
using Bakery.Data;
using Bakery.DTO;
using Bakery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bakery.Controller;


[Route("[controller]")]
[ApiController]
public class PackageController: ControllerBase
{
    private readonly MyDbContext _context;

    public PackageController(MyDbContext context)
    {
        _context = context;
    }
    
    [HttpGet(Name = "GetPackage")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        [ManualValidationFilter]
        public async Task<ActionResult<RestDTO<Package[]>>> Get(
            [FromQuery] RequestDTO<PackageDTO> input)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["traceId"] =
                    Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                if (ModelState.Keys.Any(k => k == "PageSize"))//ret
                {
                    details.Type =
                        "https://tools.ietf.org/html/rfc7231#section-6.6.2";
                    details.Status = StatusCodes.Status501NotImplemented;
                    return new ObjectResult(details) {
                        StatusCode = StatusCodes.Status501NotImplemented
                    };
                }
                else
                {
                    details.Type =
                        "https://tools.ietf.org/html/rfc7231#section-6.5.1";
                    details.Status = StatusCodes.Status400BadRequest;
                    return new BadRequestObjectResult(details);
                }
            }

            var query = _context.Packages.AsQueryable();
            if (!string.IsNullOrEmpty(input.FilterQuery))
                query = query.Where(p => p.Trackid.Contains(input.FilterQuery));
            var recordCount = await query.CountAsync();
            query = query
                    .OrderBy($"{input.SortColumn} {input.SortOrder}")
                    .Skip(input.PageIndex * input.PageSize)
                    .Take(input.PageSize);

            return new RestDTO<Package[]>()
            {
                Data = await query.ToArrayAsync(),
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                RecordCount = recordCount,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                        Url.Action(
                            null,
                            "Package",
                            new { input.PageIndex, input.PageSize },
                            Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }
    
}