using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.Log;
[BsonIgnoreExtraElements]
public class LogProperties
{
    [BsonElement("Loginfo")] 
    public Loginfo Loginfo { get; set; }

    
}