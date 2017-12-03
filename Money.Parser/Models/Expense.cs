using System;
using MongoDB.Bson;

namespace Money.Models
{
  public class Expense
  {
    public ObjectId Id { get; set; }

    public DateTime Date { get; set; }

    public string Description { get; set; }

    public double Amount { get; set; }
  }
}