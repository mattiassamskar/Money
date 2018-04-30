using System.Collections.Generic;
using MediatR;
using Money.Core.Models;

namespace Money.Core.Notifications
{
  public class ExpensesNotification : INotification
  {
    public IEnumerable<Expense> Expenses { get; set; }
  }
}