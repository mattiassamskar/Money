using System.Collections.Generic;
using System.Threading.Tasks;
using Money.Requests;
using MediatR;
using Microsoft.AspNet.SignalR;
using Money.Db;
using Money.Models;

namespace Money.Web.Hubs
{
  public class ExpenseHub : Hub, INotificationHandler<ExpenseNotification>
  {
    private readonly IDbService _dbService;

    public ExpenseHub(IDbService dbService)
    {
      _dbService = dbService;
    }

    public void Handle(ExpenseNotification notification)
    {
      GlobalHost.ConnectionManager.GetHubContext<ExpenseHub>().Clients.All
        .send(new List<Expense> {notification.Expense});
    }

    public override Task OnConnected()
    {
      Clients.Caller.send(_dbService.GetExpenses());
      return base.OnConnected();
    }
  }
}