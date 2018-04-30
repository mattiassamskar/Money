using System.Collections.Generic;
using MediatR;
using Money.Core.Models;

namespace Money.Core.Requests
{
  public class SaveExpensesRequest : IRequest
  {
    public IEnumerable<Expense> Expenses { get; set; }
  }
}