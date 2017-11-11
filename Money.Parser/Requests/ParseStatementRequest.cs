using System.Collections.Generic;
using MediatR;

namespace Money.Requests
{
  public class ParseStatementRequest : IRequest
  {
    public List<string> Lines { get; set; }
  }
}