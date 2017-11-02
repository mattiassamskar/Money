using System;
using System.Collections.Generic;
using System.Linq;
using InvoiceParser.Requests;
using InvoiceParser.StatementParsers;
using MediatR;

namespace InvoiceParser.Handlers
{
  public class ParseStatementHandler : IRequestHandler<ParseStatementRequest>
  {
    private readonly List<IStatementParser> _statementParsers;
    private readonly IMediator _mediator;

    public ParseStatementHandler(List<IStatementParser> statementParsers, IMediator mediator)
    {
      _statementParsers = statementParsers;
      _mediator = mediator;
    }

    public void Handle(ParseStatementRequest message)
    {
      var parser = _statementParsers.FirstOrDefault(sp => sp.CanParse(message.Lines));

      if (parser == null) throw new ArgumentException("Could not find suitable parser");

      message.Lines.ForEach(line =>
      {
        if (parser.TryParse(line, out var expense))
          _mediator.Publish(new ExpenseNotification {Expense = expense});
      });
    }
  }
}