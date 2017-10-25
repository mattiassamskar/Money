using System;
using System.Globalization;
using System.Linq;

namespace InvoiceParser
{
  public class Expense
  {
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }

    public static bool TryParse(string text, out Expense expense)
    {
      expense = null;

      var parts = text.Split();

      if (parts.Length < 5)
        return false;

      if (!DateTime.TryParseExact(parts[0], "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
        return false;

      if (!double.TryParse(parts.Last(), out var amount))
        return false;

      expense = new Expense {Date = dateTime, Description = text, Amount = amount};
      return true;
    }
  }
}