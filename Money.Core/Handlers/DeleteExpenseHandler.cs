using MediatR;
using Money.Core.Requests;

namespace Money.Core.Handlers
{
  public class DeleteExpenseHandler : IRequestHandler<DeleteExpenseRequest>
  {
    private readonly IDbService _dbService;

    public DeleteExpenseHandler(IDbService dbService)
    {
      _dbService = dbService;
    }

    public void Handle(DeleteExpenseRequest message)
    {
      _dbService.DeleteExpense(message.Id);
    }
  }
}