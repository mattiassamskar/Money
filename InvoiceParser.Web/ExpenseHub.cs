using InvoiceParser.Requests;
using MediatR;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace InvoiceParser.Web
{
  public class ExpenseHub : Hub, INotificationHandler<ExpenseCreatedNotification>
  {
    public void Handle(ExpenseCreatedNotification notification)
    {
      var json = JsonConvert.SerializeObject(notification.Expense);
      var hubContext = GlobalHost.ConnectionManager.GetHubContext<ExpenseHub>();
      hubContext.Clients.All.send(json);
    }
  }
}