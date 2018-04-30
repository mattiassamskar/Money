using System.Collections.Generic;
using System.Linq;
using MediatR;
using Money.Core.Models;
using Money.Core.Requests;

namespace Money.Core.Handlers
{
  public class GetExpensesHandler : IRequestHandler<GetExpensesRequest, ICollection<Expense>>
  {
    private readonly IDbService _dbService;

    public GetExpensesHandler(IDbService dbService)
    {
      _dbService = dbService;
    }

    public ICollection<Expense> Handle(GetExpensesRequest message)
    {
      var filteredExpenses = _dbService.GetFilteredExpenses(message.Filters, message.Month).ToList();
      filteredExpenses.Sort((a, b) => a.Date.CompareTo(b.Date));
      return filteredExpenses;
    }
  }
}