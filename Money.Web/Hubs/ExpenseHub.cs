using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNet.SignalR;
using Money.Web.Notifications;
using Money.Web.Requests;

namespace Money.Web.Hubs
{
  public class ExpenseHub : Hub, INotificationHandler<ExpensesNotification>
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

    public void Handle(ExpensesNotification notification)
    {
      GlobalHost.ConnectionManager.GetHubContext<ExpenseHub>().Clients.All
        .send(notification.Expenses);
    }
  }
}