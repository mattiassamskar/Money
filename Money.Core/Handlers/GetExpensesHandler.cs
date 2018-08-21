using System.Collections.Generic;
using System.Linq;
using MediatR;
using Money.Core.Models;
using Money.Core.Requests;

namespace Money.Core.Handlers
{
  public class GetExpensesHandler : RequestHandler<GetExpensesRequest, ICollection<Expense>>
  {
    private readonly IDbService _dbService;

    public GetExpensesHandler(IDbService dbService)
    {
      _dbService = dbService;
    }
    protected override ICollection<Expense> Handle(GetExpensesRequest request)
    {
      var filteredExpenses = _dbService.GetFilteredExpenses(request.Filters, request.Month).ToList();
      filteredExpenses.Sort((a, b) => a.Date.CompareTo(b.Date));
      return filteredExpenses;
    }
  }
}