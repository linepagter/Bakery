using MongoDB.Bson.Serialization.Attributes;

namespace Bakery.Log;
public class Loginfo
{
    public string Operation { get; set; }
    public string Timestamp { get; set; }
    
    public string User { get; set; }
    


}