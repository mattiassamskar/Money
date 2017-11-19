using System.Collections.Generic;
using MediatR;
using Money.Models;

namespace Money.Web.Requests
{
  public class SaveExpensesRequest : IRequest
  {
    public IEnumerable<Expense> Expenses { get; set; }
  }
}