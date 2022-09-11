using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Money.Core.Models
{
  public class Filter
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Text { get; set; }
  }
}