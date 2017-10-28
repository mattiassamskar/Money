using System.Collections.Generic;
using InvoiceParser.Models;
using MediatR;

namespace InvoiceParser.Requests
{
  public class ParseCirclekInvoiceRequest : IRequest<IEnumerable<Expense>>
  {
    public string Text { get; set; }
  }
}