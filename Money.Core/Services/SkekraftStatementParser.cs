using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Money.Core.Models;

namespace Money.Core.Services
{
  public class SkekraftStatementParser : IStatementParser
  {
    public bool CanParse(Statement statement)
    {
      return statement.Lines.Any(line => line.Contains("Skellefteå Kraft AB"));
    }

    public IEnumerable<Expense> Parse(Statement statement)
    {
      var expensesDate = GetExpensesDate(statement.Lines);

      foreach (var line in statement.Lines)
      {
        Expense expense;
        if (TryParse(line, out expense))
        {
          expense.Date = expensesDate;
          yield return expense;
        }
      }
    }

    private DateTime GetExpensesDate(ICollection<string> lines)
    {
      var invoiceDateLine = lines.First(line => line.StartsWith("Fakturadatum"));

      DateTime dateTime;
      DateTime.TryParseExact(invoiceDateLine.Substring(13), "yyyy-MM-dd", new CultureInfo("sv-SE"), DateTimeStyles.None, out dateTime);

      return dateTime;
    }

    private bool TryParse(string line, out Expense expense)
    {
      expense = null;

      if (!line.StartsWith("Elhandel"))
        return false;

      if (!line.EndsWith("SEK")) return false;

      Double amount;
      var amountMatch = Regex.Match(line, @"\d*\s*\d+,\d{2}");
      if (!amountMatch.Success || !double.TryParse(amountMatch.Value, NumberStyles.Any, new CultureInfo("sv-SE"), out amount))
        return false;
      expense = new Expense { Description = line.Split(' ').First(), Amount = amount };
      return true;
    }
  }
}