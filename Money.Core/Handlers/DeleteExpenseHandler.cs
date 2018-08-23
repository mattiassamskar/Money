using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Money.Core.Requests;

namespace Money.Core.Handlers
{
  public class DeleteExpenseHandler : AsyncRequestHandler<DeleteExpenseRequest>
  {
    private readonly IDbService _dbService;

    public DeleteExpenseHandler(IDbService dbService)
    {
      _dbService = dbService;
    }

    protected override Task Handle(DeleteExpenseRequest request, CancellationToken cancellationToken)
    {
      return Task.Run(() => _dbService.DeleteExpense(request.Id));
    }
  }
}