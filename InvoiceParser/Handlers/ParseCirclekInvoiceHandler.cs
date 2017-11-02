using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using InvoiceParser.Models;
using InvoiceParser.Requests;
using MediatR;

namespace InvoiceParser.Handlers
{
  public class ParseCirclekInvoiceHandler : IRequestHandler<ParseCirclekInvoiceRequest, IEnumerable<Expense>>
  {
    private readonly IMediator _mediator;

    public ParseCirclekInvoiceHandler(IMediator mediator)
    {
      _mediator = mediator;
    }

    public IEnumerable<Expense> Handle(ParseCirclekInvoiceRequest message)
    {
      return GetExpensesFromText(message.Text).ToList();
    }

    private IEnumerable<Expense> GetExpensesFromText(string text)
    {
      var lines = text.Split('\n');
      var withinExpenses = false;

      foreach (var line in lines)
      {
        if (line.StartsWith("MASTERCARD TRANSAKTIONER"))
        {
          withinExpenses = true;
          continue;
        }

        if (line.StartsWith("SUMMA MASTERCARD TRANSAKTIONER"))
        {
          withinExpenses = false;
          continue;
        }

        if (withinExpenses && TryParse(line, out var expense))
        {
          _mediator.Publish(new ExpenseCreatedNotification {Expense = expense});
          yield return expense;
        }
      }
    }

    private bool TryParse(string line, out Expense expense)
    {
      expense = null;
      var parts = line.Split();

      if (parts.Length < 5)
        return false;

      if (!DateTime.TryParseExact(parts[0], "yyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
        return false;

      if (!double.TryParse(parts.Last(), out var amount))
        return false;

      expense = new Expense { Date = dateTime, Description = line, Amount = amount };
      return true;
    }
  }
}