using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Money.Core.Models;
using Money.Core.Requests;
using Money.Core.Services;

namespace Money.Core.Handlers
{
  public class ParsePdfHandler : RequestHandler<ParsePdfRequest, ICollection<Expense>>
  {
    private readonly IStatementService _statementService;

    public ParsePdfHandler(IStatementService statementService)
    {
      _statementService = statementService;
    }

    protected override ICollection<Expense> Handle(ParsePdfRequest request)
    {
      return _statementService.Parse(request.Bytes).ToList();
    }
  }
}