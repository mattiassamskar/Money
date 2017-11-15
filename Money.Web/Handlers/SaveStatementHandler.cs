using MediatR;
using Money.Db;
using Money.Web.Notifications;

namespace Money.Web.Handlers
{
  public class SaveStatementHandler : INotificationHandler<ExpensesNotification>
  {
    private readonly IDbService _dbService;

    public SaveStatementHandler(IDbService dbService)
    {
      _dbService = dbService;
    }

    public void Handle(ExpensesNotification notification)
    {
      foreach (var expense in notification.Expenses)
      {
        _dbService.AddExpense(expense);
      }
    }
  }
}