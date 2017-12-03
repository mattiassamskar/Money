using System.Collections.Generic;
using MediatR;
using Money.Models;
using MongoDB.Bson;

namespace Money.Web.Requests
{
  public class GetExpensesRequest : IRequest<ICollection<Expense>>
  {
    public IEnumerable<string> Filters { get; set; }
    public string Month { get; set; }
  }

  public class DeleteExpenseRequest : IRequest
  {
    public ObjectId Id { get; set; }
  }
}