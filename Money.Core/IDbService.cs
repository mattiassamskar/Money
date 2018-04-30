using System.Collections.Generic;
using Money.Core.Models;

namespace Money.Core
{
  public interface IDbService
  {
    ICollection<Expense> GetExpenses();
    ICollection<Expense> GetFilteredExpenses(IEnumerable<string> filters, string month);
    void AddExpenses(IEnumerable<Expense> expenses);
    void AddExpense(Expense expense);
    void DeleteExpense(string objectId);
  }
}