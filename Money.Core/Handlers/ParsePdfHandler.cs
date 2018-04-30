using System.Collections.Generic;
using System.Linq;
using MediatR;
using Money.Core.Models;
using Money.Core.Requests;
using Money.Core.Services;

namespace Money.Core.Handlers
{
  public class ParsePdfHandler : IRequestHandler<ParsePdfRequest, ICollection<Expense>>
  {
    private readonly IStatementService _statementService;

    public ParsePdfHandler(IStatementService statementService)
    {
      _statementService = statementService;
    }
    public ICollection<Expense> Handle(ParsePdfRequest message)
    {
      return _statementService.Parse(message.Bytes).ToList();
    }
  }
}