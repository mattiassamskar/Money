using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Money.Core.Models;

namespace Money.Core.Services
{
  public class CirclekStatementParser : IStatementParser
  {
    public bool CanParse(Statement statement)
    {
      return statement.Lines.Any(line => line.Contains("Circle K Mastercard") || line.Contains("Circle K MasterCard"));
    }

    public IEnumerable<Expense> Parse(Statement statement)
    {
      foreach (var line in statement.Lines)
      {
        Expense expense;
        if (TryParse(line, out expense))
          yield return expense;
      }
    }

    private bool TryParse(string line, out Expense expense)
    {
      expense = null;
      var parts = line.Split();

      if (parts.Length < 5)
        return false;

      DateTime dateTime;
      if (!DateTime.TryParseExact(parts[0], "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
        return false;

      Double amount;
      if (!double.TryParse(parts.Last(), NumberStyles.Any, new CultureInfo("sv-SE"), out amount) || amount < 0)
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