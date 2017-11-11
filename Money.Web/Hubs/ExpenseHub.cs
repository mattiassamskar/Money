using Money.Requests;
using MediatR;
using Microsoft.AspNet.SignalR;

namespace Money.Web.Hubs
{
  public class ExpenseHub : Hub, INotificationHandler<ExpenseNotification>
  {
    public void Handle(ExpenseNotification notification)
    {
      GlobalHost.ConnectionManager.GetHubContext<ExpenseHub>().Clients.All.send(notification.Expense);
    }
  }
}