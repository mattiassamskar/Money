using System.Linq;
using Money.Core.Models;
using Money.Core.Services;
using Xunit;
using Xunit.Abstractions;

namespace Money.Tests
{
  public class SeomStatementParserTests
  {
    private readonly ITestOutputHelper _output;

    public SeomStatementParserTests(ITestOutputHelper output)
    {
      _output = output;
    }

    [Fact]
    public void CanParse_WhenAbleToParseNewStatement_ReturnsTrue()
    {
      var statement = new Statement
      {
        Lines = new[] { "Fakturadatum             7 okt 2024", "Vatten 15,90 kr" }
      };

      var result = new SeomStatementParser().Parse(statement);
      Assert.True(result.Any());
      _output.WriteLine(result.FirstOrDefault().Date.ToString(), result.FirstOrDefault().Amount.ToString());
    }
  }
}