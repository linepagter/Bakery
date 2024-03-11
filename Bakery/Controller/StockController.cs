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
public class StockController: ControllerBase
{
    private readonly MyDbContext _context;

    public StockController(MyDbContext context)
    {
        _context = context;
    }
    
    [HttpGet(Name = "GetStocks")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        [ManualValidationFilter]
        public async Task<ActionResult<RestDTO<Stock[]>>> Get(
            [FromQuery] RequestDTO<StockDTO> input)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["StockId"] =
                    Activity.Current?.Id ?? HttpContext.TraceIdentifier;
                if (ModelState.Keys.Any(k => k == "PageSize"))
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

            var query = _context.Stocks.AsQueryable();
            if (!string.IsNullOrEmpty(input.FilterQuery))
                query = query.Where(b => b.StockId.ToString().Contains(input.FilterQuery));
            var recordCount = await query.CountAsync();
            query = query
                    .OrderBy($"{input.SortColumn} {input.SortOrder}")
                    .Skip(input.PageIndex * input.PageSize)
                    .Take(input.PageSize);

            return new RestDTO<Stock[]>()
            {
                Data = await query.ToArrayAsync(),
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                RecordCount = recordCount,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                        Url.Action(
                            null,
                            "Stock",
                            new { input.PageIndex, input.PageSize },
                            Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }

        [HttpPost(Name = "UpdateStock")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<Stock?>> Post(StockDTO model)
        {
            var stock = await _context.Stocks
                .Where(b => b.StockId == model.Id)
                .FirstOrDefaultAsync();
            if (stock != null)
            {
                if (!string.IsNullOrEmpty(model.Id))
                    stock.StockId = model.Id;
                _context.Stocks.Update(stock);
                await _context.SaveChangesAsync();
            };

            return new RestDTO<Stock?>()
            {
                Data = stock,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                            Url.Action(
                                null,
                                "Stock",
                                model,
                                Request.Scheme)!,
                            "self",
                            "POST"),
                }
            };
        }

        [HttpDelete(Name = "DeleteStock")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<Stock?>> Delete(int id)
        {
            var stock = await _context.Stocks
                .Where(b => b.StockId == id)
                .FirstOrDefaultAsync();
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            };

            return new RestDTO<Stock?>()
            {
                Data = stock,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                            Url.Action(
                                null,
                                "Stock",
                                id,
                                Request.Scheme)!,
                            "self",
                            "DELETE"),
                }
            };
        }
    
}