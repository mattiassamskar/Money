using System.Collections.Generic;
using MediatR;
using Money.Models;

namespace Money.Web.Requests
{
  public class GetExpensesRequest : IRequest<ICollection<Expense>>
  {
  }
}