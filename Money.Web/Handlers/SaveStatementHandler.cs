using MediatR;
using Money.Db;
using Money.Web.Requests;

namespace Money.Web.Handlers
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