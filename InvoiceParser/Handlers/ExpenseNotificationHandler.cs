using System.Diagnostics;
using InvoiceParser.Models;
using MediatR;

namespace InvoiceParser.Handlers
{
  public class ExpenseNotificationHandler : INotificationHandler<Expense>
  {
    public void Handle(Expense expense)
    {
      Debug.Print(expense.Date + "; " + expense.Description + "; " + expense.Amount);
    }
  }
}