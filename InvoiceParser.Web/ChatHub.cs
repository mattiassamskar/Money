using InvoiceParser.Requests;
using MediatR;
using Microsoft.AspNet.SignalR;

namespace InvoiceParser.Web
{
  public class ChatHub : Hub, INotificationHandler<ExpenseCreatedNotification>
  {
    public void Handle(ExpenseCreatedNotification notification)
    {
      var hubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
      hubContext.Clients.All.send(notification.Expense.Description);
    }
  }
}