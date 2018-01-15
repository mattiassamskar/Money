using MediatR;
using Money.Db;
using Money.Web.Requests;

namespace Money.Web.Handlers
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