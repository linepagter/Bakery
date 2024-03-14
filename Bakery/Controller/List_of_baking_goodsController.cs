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
public class List_of_baking_goodsController: ControllerBase
{
    private readonly MyDbContext _context;

    public List_of_baking_goodsController(MyDbContext context)
    {
        _context = context;
    }
    
     [HttpGet(Name = "GetListOfBakingGoods")]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 60)]
        [ManualValidationFilter]
        public async Task<ActionResult<RestDTO<ListOfBakingGoods[]>>> Get(
            [FromQuery] RequestDTO<ListOfBakingGoodsDTO> input)
        {
            if (!ModelState.IsValid)
            {
                var details = new ValidationProblemDetails(ModelState);
                details.Extensions["ListId"] =
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

            var query = _context.ListOfBakingGoods.AsQueryable();
            if (!string.IsNullOrEmpty(input.FilterQuery))
                query = query.Where(b => b.ListId.ToString().Contains(input.FilterQuery));
            var recordCount = await query.CountAsync();
            //query = query
                    // .OrderBy($"{input.SortColumn} {input.SortOrder}")
                    // .Skip(input.PageIndex * input.PageSize)
                    // .Take(input.PageSize);

            return new RestDTO<ListOfBakingGoods[]>()
            {
                Data = await query.ToArrayAsync(),
                PageIndex = input.PageIndex,
                PageSize = input.PageSize,
                RecordCount = recordCount,
                Links = new List<LinkDTO> {
                    new LinkDTO(
                        Url.Action(
                            null,
                            "List_of_baking_goods",
                            new { input.PageIndex, input.PageSize },
                            Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }

        [HttpPost(Name = "UpdateListOfBakingGoods")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<ListOfBakingGoods?>> Post(ListOfBakingGoodsDTO model)
        {
            var listBG = await _context.ListOfBakingGoods
                .Where(b => b.ListId == model.ListId)
                .FirstOrDefaultAsync();
            if (listBG != null)
            {
                // if (!string.IsNullOrEmpty(model.ListId))
                    listBG.ListId = model.ListId;
                _context.ListOfBakingGoods.Update(listBG);
                await _context.SaveChangesAsync();
            };

            return new RestDTO<ListOfBakingGoods?>()
            {
                Data = listBG,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                            Url.Action(
                                null,
                                "List_of_baking_goods",
                                model,
                                Request.Scheme)!,
                            "self",
                            "POST"),
                }
            };
        }

        [HttpDelete(Name = "DeleteListOfBakingGoods")]
        [ResponseCache(NoStore = true)]
        public async Task<RestDTO<ListOfBakingGoods?>> Delete(int id)
        {
            var listBG = await _context.ListOfBakingGoods
                .Where(b => b.ListId == id)
                .FirstOrDefaultAsync();
            if (listBG != null)
            {
                _context.ListOfBakingGoods.Remove(listBG);
                await _context.SaveChangesAsync();
            };

            return new RestDTO<ListOfBakingGoods?>()
            {
                Data = listBG,
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                            Url.Action(
                                null,
                                "List_of_baking_goods",
                                id,
                                Request.Scheme)!,
                            "self",
                            "DELETE"),
                }
            };
        }
    
    
}