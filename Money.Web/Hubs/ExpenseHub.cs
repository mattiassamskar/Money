using System.Collections.Generic;
using System.Threading.Tasks;
using Money.Requests;
using MediatR;
using Microsoft.AspNet.SignalR;
using Money.Models;
using Money.Web.Requests;

namespace Money.Web.Hubs
{
  public class ExpenseHub : Hub, INotificationHandler<ExpenseNotification>
  {
    private readonly IMediator _mediator;

    public ExpenseHub(IMediator mediator)
    {
      _mediator = mediator;
    }

    public override Task OnConnected()
    {
      var expenses = _mediator.Send(new GetExpensesRequest()).Result;
      Clients.Caller.send(expenses);

      return base.OnConnected();
    }

    public void Handle(ExpenseNotification notification)
    {
      GlobalHost.ConnectionManager.GetHubContext<ExpenseHub>().Clients.All
        .send(new List<Expense> { notification.Expense });
    }
  }
}