using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Money.Core.Models;

namespace Money.Core.Services
{
  public class SeomStatementParser : IStatementParser
  {
    public bool CanParse(Statement statement)
    {
      return statement.Lines.Any(line => line.Contains("Sollentuna Energi och Miljö AB"));
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
      DateTime.TryParseExact(invoiceDateLine.Substring(13).Trim(), "d MMM yyyy", CustomCultureInfo, DateTimeStyles.None, out dateTime);

      return dateTime;
    }

    private bool TryParse(string line, out Expense expense)
    {
      expense = null;

      if (
        !line.StartsWith("Vatten") &&
        !line.StartsWith("Hushållsavfall") &&
        !line.StartsWith("Fjärrvärme") &&
        !line.StartsWith("Elnät"))
        return false;

      if (!line.EndsWith("kr")) return false;

      Double amount;
      var amountMatch = Regex.Match(line, @"\d*\s*\d+,\d{2}");
      if (!amountMatch.Success || !double.TryParse(amountMatch.Value, NumberStyles.Any, new CultureInfo("sv-SE"), out amount))
        return false;
      expense = new Expense { Description = line.Split(' ').First(), Amount = amount };
      return true;
    }

    private static CultureInfo CustomCultureInfo
    {
      get
      {
        var cultureInfo = CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(c => c.LCID == 29).Clone() as CultureInfo;
        cultureInfo.DateTimeFormat.AbbreviatedMonthNames = cultureInfo.DateTimeFormat.AbbreviatedMonthNames.Select(x => x.TrimEnd('.')).ToArray();
        cultureInfo.DateTimeFormat.AbbreviatedMonthGenitiveNames = cultureInfo.DateTimeFormat.AbbreviatedMonthGenitiveNames.Select(x => x.TrimEnd('.')).ToArray();
        return cultureInfo;
      }
    }
  }
}