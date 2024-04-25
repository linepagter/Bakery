using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.Log;
[BsonIgnoreExtraElements]
public class LogEntry
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Level")] public string Level { get; set; } = "";
    
    [BsonElement("Properties")] 
    public LogProperties Properties { get; set; }
}