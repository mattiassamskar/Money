using System;
using Money.Db;
using Money.Models;
using Xunit;

namespace Money.Tests
{
  public class Tests
  {
    [Fact]
    public void Dbtest()
    {
      var expense = new Expense
      {
        Amount = 123.4,
        Date = DateTime.Parse("2017-11-11 15:07:37"),
        Description = "Description 1 Description 2"
      };

      var db = new MongoDbService();
      var expenses = db.GetExpenses();
    }
  }
}