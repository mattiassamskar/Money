using System.Collections.Generic;
using System.Linq;
using Money.Core;
using Money.Core.Models;

namespace Money.Db
{
  public class MockDbService : IDbService
  {
    private readonly List<Expense> _expenses;
    private readonly List<Filter> _filters;

    public MockDbService()
    {
      _expenses = new List<Expense>();
      _filters = new List<Filter>();
    }

    public ICollection<Expense> GetExpenses()
    {
      return _expenses;
    }

    public ICollection<Expense> GetFilteredExpenses(IEnumerable<string> filters, string month)
    {
      return filters.Any() ? filters.SelectMany(filter => _expenses.FindAll(expense => expense.Description.ToLower().Contains(filter.ToLower()))).ToList() : GetExpenses();
    }

    public void AddExpenses(IEnumerable<Expense> expenses)
    {
      _expenses.AddRange(expenses);
    }

    public void AddExpense(Expense expense)
    {
      _expenses.Add(expense);
    }

    public void DeleteExpense(string objectId)
    {
      _expenses.Remove(_expenses.Single(expense => expense.Id == objectId));
    }

    public ICollection<Filter> GetFilters()
    {
      return _filters;
    }

    public void AddFilter(Filter filter)
    {
      _filters.Add(filter);
    }

    public void DeleteFilter(string objectId)
    {
      _filters.Remove(_filters.Single(filter => filter.Id == objectId));
    }
  }
}