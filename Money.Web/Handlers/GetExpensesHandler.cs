using System.Collections.Generic;
using MediatR;
using Money.Db;
using Money.Models;
using Money.Web.Requests;

namespace Money.Web.Handlers
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
      return _dbService.GetExpenses();
    }
  }
}