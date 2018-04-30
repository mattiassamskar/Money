using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Money.Core.Models
{
  public class Expense
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; }

    public double Amount { get; set; }
  }
}