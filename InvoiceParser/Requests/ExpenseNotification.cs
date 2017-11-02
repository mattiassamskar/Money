using InvoiceParser.Models;
using MediatR;

namespace InvoiceParser.Requests
{
  public class ExpenseNotification : INotification
  {
    public Expense Expense { get; set; }
  }
}