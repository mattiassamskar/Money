using Money.Models;
using MediatR;

namespace Money.Requests
{
  public class ExpenseNotification : INotification
  {
    public Expense Expense { get; set; }
  }
}