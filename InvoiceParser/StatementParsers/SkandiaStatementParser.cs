using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using InvoiceParser.Models;

namespace InvoiceParser.StatementParsers
{
  public class SkandiaStatementParser : IStatementParser
  {
    public bool CanParse(List<string> lines)
    {
      return lines.Any(line => line.Contains("Kontot omfattas av den statliga insättningsgarantin"));
    }

    public bool TryParse(string line, out Expense expense)
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
      expense = new Expense { Date = dateTime, Description = line, Amount = amount };
      return true;
    }
  }
}