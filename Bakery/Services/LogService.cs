using Azure;
using Bakery.Controller;
using Bakery.Log;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Bakery.Services;

public class LogService
{
    private readonly IMongoCollection<LogEntry> _logCollection;
    private readonly string _connectionString = "mongodb://localhost:27017";
    private readonly string _databaseName = "Serilog_Bakery";
    private readonly string _collectionName = "logs";

    public LogService()
    {
        var mongoClient = new MongoClient(_connectionString);
        var database = mongoClient.GetDatabase(_databaseName);
        _logCollection = database.GetCollection<LogEntry>(_collectionName);
        Console.WriteLine(mongoClient.Settings);
    }
    
    
    public async Task<List<LogEntry>> GetAsync(string? operation, string? timestamp, string? user)
    {
        var filterBuilder = Builders<LogEntry>.Filter;
        var filter = filterBuilder.Eq(x => x.Level, "Information");

        if (!string.IsNullOrEmpty(operation))
        {
            filter &= filterBuilder.Eq(x => x.Properties.Loginfo.Operation, operation);
        }

        if (!string.IsNullOrEmpty(timestamp))
        {
            filter &= filterBuilder.Eq(x => x.Properties.Loginfo.Timestamp, timestamp);
        }

        if (!string.IsNullOrEmpty(user))
        {
            filter &= filterBuilder.Eq(x => x.Properties.Loginfo.User, user);
        }

        var query = _logCollection.Find(filter);
        return await query.ToListAsync();
    }
}