using Bakery.Log;
using Bakery.Models;
using Bakery.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
namespace Bakery.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class LogController : ControllerBase
    {
        private readonly LogService _logService;

        public LogController(LogService logService)
        {
            _logService = logService;
        }
        [Authorize(Roles = $"{UserRoles.Administrator}")]
        [HttpGet("Get")]
        public async Task<IActionResult> Search(string? Operation, string? Timestamp, string? User)
        {
            var logs = await _logService.GetAsync(Operation, Timestamp, User);
            return Ok(logs);
        }
    }
}