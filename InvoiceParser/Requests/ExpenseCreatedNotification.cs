using InvoiceParser.Models;
using MediatR;

namespace InvoiceParser.Requests
{
  public class ExpenseCreatedNotification : INotification
  {
    public Expense Expense { get; set; }
  }
}