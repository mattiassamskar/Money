using Money.Core.Models;
using Money.Core.Services;
using Xunit;

namespace Money.Tests
{
  public class CirclekStatementParserTests
  {
    [Fact]
    public void CanParse_WhenAbleToParseNewStatement_ReturnsTrue()
    {
      // Arrange
      var statement = new Statement
      {
        Lines = new[] { "Circle K Mastercard" }
      };

      // Act and assert
      Assert.True(new CirclekStatementParser().CanParse(statement));
    }

    [Fact]
    public void CanParse_WhenAbleToParseOldStatement_ReturnsTrue()
    {
      // Arrange
      var statement = new Statement
      {
        Lines = new[] { "Circle K MasterCard" }
      };

      // Act and assert
      Assert.True(new CirclekStatementParser().CanParse(statement));
    }
  }
}