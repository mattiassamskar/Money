using System.Collections.Generic;
using MediatR;
using Money.Models;
using Money.Web.Requests;

namespace Money.Web.Handlers
{
  public class ParsePdfHandler : IRequestHandler<ParsePdfRequest, IEnumerable<Expense>>
  {
    private readonly IStatementService _statementService;

    public ParsePdfHandler(IStatementService statementService)
    {
      _statementService = statementService;
    }
    public IEnumerable<Expense> Handle(ParsePdfRequest message)
    {
      return _statementService.Parse(message.Bytes);
    }
  }
}