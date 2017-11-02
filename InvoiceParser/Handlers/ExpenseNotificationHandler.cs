using System.Diagnostics;
using InvoiceParser.Requests;
using MediatR;

namespace InvoiceParser.Handlers
{
  public class ExpenseNotificationHandler : INotificationHandler<ExpenseCreatedNotification>
  {

    public void Handle(ExpenseCreatedNotification notification)
    {
      Debug.Print(notification.Expense.Date + "; " + notification.Expense.Description + "; " + notification.Expense.Amount);
    }
  }
}