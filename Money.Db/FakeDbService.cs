using System;
using System.Collections.Generic;
using System.Linq;
using Money.Core;
using Money.Core.Models;

namespace Money.Db
{
  public class FakeDbService : IDbService
  {
    public void AddExpense(Expense expense)
    {
      throw new NotImplementedException();
    }

    public void AddExpenses(IEnumerable<Expense> expenses)
    {
      throw new NotImplementedException();
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
      var expenses = new List<Expense>
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

      return filters.Any() 
        ? expenses.Where(expense => filters.Any(filter => expense.Description.Contains(filter))).ToList() 
        : expenses;
    }
  }
}