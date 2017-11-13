using System;
using Money.Db;
using Money.Models;
using Money.StatementParsers;
using Xunit;

namespace Money.Tests
{
  public class Tests
  {
    [Fact]
    public void SkandiaStatementParser_TryParse_FindsDescription()
    {
      // Arrange
      const string line = "2017-02-03 Description1 description2 -223,00 19 876,54";

      // Act
      new SkandiaStatementParser().TryParse(line, out var expense);

      // Assert
      Assert.Equal("Description1 description2", expense.Description);
    }

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