using MediatR;
using Money.Db;
using Money.Requests;

namespace Money.Web.Handlers
{
  public class SaveStatementHandler : INotificationHandler<ExpenseNotification>
  {
    private readonly IDbService _dbService;

    public SaveStatementHandler(IDbService dbService)
    {
      _dbService = dbService;
    }

    public void Handle(ExpenseNotification notification)
    {
      _dbService.AddExpense(notification.Expense);
    }
  }
}