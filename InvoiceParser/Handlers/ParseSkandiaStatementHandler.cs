using System;
using System.Collections.Generic;
using System.Linq;
using InvoiceParser.Models;
using InvoiceParser.Requests;
using MediatR;

namespace InvoiceParser.Handlers
{
  public class ParseSkandiaStatementHandler : IRequestHandler<ParseSkandiaStatementRequest, IEnumerable<Expense>>
  {
    public IEnumerable<Expense> Handle(ParseSkandiaStatementRequest message)
    {
      return GetExpensesFromText(message.Text).ToList();
    }

    private IEnumerable<Expense> GetExpensesFromText(string text)
    {
      throw new NotImplementedException();
    }
  }
}