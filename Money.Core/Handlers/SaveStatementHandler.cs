using MediatR;
using Money.Core.Requests;

namespace Money.Core.Handlers
{
  public class SaveStatementHandler : IRequestHandler<SaveExpensesRequest>
  {
    private readonly IDbService _dbService;

    public SaveStatementHandler(IDbService dbService)
    {
      _dbService = dbService;
    }

    public void Handle(SaveExpensesRequest message)
    {
      _dbService.AddExpenses(message.Expenses);
    }
  }
}