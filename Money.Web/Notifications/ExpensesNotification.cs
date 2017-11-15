using System.Collections.Generic;
using MediatR;
using Money.Models;

namespace Money.Web.Notifications
{
  public class ExpensesNotification : INotification
  {
    public IEnumerable<Expense> Expenses { get; set; }
  }
}