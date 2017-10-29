using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using InvoiceParser.Models;
using InvoiceParser.Requests;
using MediatR;

namespace InvoiceParser.Handlers
{
  public class ParseSkandiaStatementHandler : IRequestHandler<ParseSkandiaStatementRequest, IEnumerable<Expense>>
  {
    public IEnumerable<Expense> Handle(ParseSkandiaStatementRequest message)
    {
      return GetExpensesFromText(message.Text);
    }

    private IEnumerable<Expense> GetExpensesFromText(string text)
    {
      var lines = text.Split('\n');

      foreach (var line in lines)
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

      if (!amountMatch.Success || !double.TryParse(amountMatch.Value, out var amount))
        return false;

      amount = Math.Abs(amount);
      expense = new Expense {Date = dateTime, Description = line, Amount = amount};
      return true;
    }
  }
}