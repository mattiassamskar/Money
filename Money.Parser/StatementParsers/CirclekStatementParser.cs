using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Money.Models;

namespace Money.StatementParsers
{
  public class CirclekStatementParser : IStatementParser
  {
    public bool CanParse(List<string> lines)
    {
      return lines.Any(line => line.Contains("Circle K MasterCard"));
    }

    public bool TryParse(string line, out Expense expense)
    {
      expense = null;
      var parts = line.Split();

      if (parts.Length < 5)
        return false;

      if (!DateTime.TryParseExact(parts[0], "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
        return false;

      if (!double.TryParse(parts.Last(), NumberStyles.Any, new CultureInfo("sv-SE"), out var amount) || amount < 0)
        return false;

      var description = string.Empty;

      for (var i = 1; i < parts.Length - 2; i++)
      {
        description += " " + parts[i];
      }

      expense = new Expense { Date = dateTime, Description = description, Amount = amount };
      return true;
    }
  }
}