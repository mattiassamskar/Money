using System;
using System.Collections.Generic;
using System.Linq;
using Money.Core;
using Money.Core.Models;

namespace Money.Db
{
  public class FakeDbService : IDbService
  {
    private List<Expense> _expenses = new List<Expense>
      {
        new Expense
        {
          Amount = 100,
          Date = DateTime.Now,
          Description = "Expense 1"
        },
        new Expense
        {
          Amount = 200,
          Date = DateTime.Now,
          Description = "Expense 2"
        }
      };

    public void AddExpense(Expense expense)
    {
      throw new NotImplementedException();
    }

    public void AddExpenses(IEnumerable<Expense> expenses)
    {
      _expenses.AddRange(expenses);
    }

    public void DeleteExpense(string objectId)
    {
      throw new NotImplementedException();
    }

    public ICollection<Expense> GetExpenses()
    {
      throw new NotImplementedException();
    }

    public ICollection<Expense> GetFilteredExpenses(IEnumerable<string> filters, string month)
    {
      return filters.Any()
        ? _expenses.Where(expense => filters.Any(filter => expense.Description.Contains(filter))).ToList()
        : _expenses;
    }
  }
}