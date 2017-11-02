using InvoiceParser.Requests;
using MediatR;
using Microsoft.AspNet.SignalR;

namespace InvoiceParser.Web
{
  public class ExpenseHub : Hub, INotificationHandler<ExpenseCreatedNotification>
  {
    public void Handle(ExpenseCreatedNotification notification)
    {
      GlobalHost.ConnectionManager.GetHubContext<ExpenseHub>().Clients.All.send(notification.Expense);
    }
  }
}