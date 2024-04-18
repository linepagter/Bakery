using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Bakery.Controller;

[ApiController]
[Route("[controller]")]
public class LogController : ControllerBase
{
    private readonly IMongoCollection<LogEntry> _logCollection;

    public LogController(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("Serilog_Bakery");
        _logCollection = database.GetCollection<LogEntry>("logs");
    }

    [HttpGet("GetLog")]
    public async Task<IActionResult> SearchLogs([FromQuery] LogSearchFilter filter)
    {
        // Validate the filter parameters
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var builder = Builders<LogEntry>.Filter;
        var filterDefinition = builder.Empty; // Initial filter definition

        // Apply filters based on the provided parameters
        if (!string.IsNullOrEmpty(filter.UserId))
        {
            filterDefinition &= builder.Eq(entry => entry.UserId, filter.UserId);
        }

        if (filter.Timestamp.HasValue)
        {
            filterDefinition &= builder.Gte(entry => entry.Timestamp, filter.Timestamp.Value);
        }
        
        if (filter.OperationType.HasValue)
        {
            filterDefinition &= builder.Eq(entry => entry.OperationType, filter.OperationType.Value);
        }

        // Query the MongoDB collection with the filter
        var logEntries = await _logCollection.Find(filterDefinition).ToListAsync();

        return Ok(logEntries);
    }
}

public class LogSearchFilter
{
    public string UserId { get; set; }
    public DateTime? Timestamp { get; set; }
    public OperationType? OperationType { get; set; }
}

public class LogEntry
{
    public ObjectId Id { get; set; }
    public string UserId { get; set; }
    public DateTime Timestamp { get; set; }
    public OperationType OperationType { get; set; }

}

public enum OperationType
{
    POST,
    PUT,
    DELETE
}