using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Money.Models;

namespace Money.StatementParsers
{
  public class SkandiaStatementParser : IStatementParser
  {
    public bool CanParse(Statement statement)
    {
      return statement.Lines.Any(line => line.Contains("Kontot omfattas av den statliga insättningsgarantin"));
    }

    public IEnumerable<Expense> Parse(Statement statement)
    {
      foreach (var line in statement.Lines)
      {
        if (TryParse(line, out var expense))
          yield return expense;
      }
    }

    private bool TryParse(string line, out Expense expense)
    {
      expense = null;
      var parts = line.Split();

      if (parts.Length < 4)
        return false;

      if (!DateTime.TryParseExact(parts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None,
        out var dateTime))
        return false;

      var amountMatch = Regex.Match(line, @"-\d*\s*\d+,\d{2}");
      if (!amountMatch.Success || !double.TryParse(amountMatch.Value, NumberStyles.Any, new CultureInfo("sv-SE"), out var amount))
        return false;

      amount = Math.Abs(amount);

      var description = Regex.Match(line, @"(?<=.{11}).+(?= -\d*\s*\d+,\d{2}.+)").Value;
      expense = new Expense { Date = dateTime, Description = description, Amount = amount };
      return true;
    }
  }
}