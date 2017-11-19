using System.Collections.Generic;
using Money.Models;

namespace Money.Db
{
  public interface IDbService
  {
    ICollection<Expense> GetExpenses();
    void AddExpenses(IEnumerable<Expense> expenses);
    void AddExpense(Expense expense);
  }
}