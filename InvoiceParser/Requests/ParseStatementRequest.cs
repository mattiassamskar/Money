using System.Collections.Generic;
using MediatR;

namespace InvoiceParser.Requests
{
  public class ParseStatementRequest : IRequest
  {
    public List<string> Lines { get; set; }
  }
}