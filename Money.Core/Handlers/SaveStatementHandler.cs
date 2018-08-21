using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Money.Core.Requests;

namespace Money.Core.Handlers
{
  public class SaveStatementHandler : AsyncRequestHandler<SaveExpensesRequest>
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

    protected override Task Handle(SaveExpensesRequest request, CancellationToken cancellationToken)
    {
      return Task.Run(() => _dbService.AddExpenses(request.Expenses));
    }
  }
}