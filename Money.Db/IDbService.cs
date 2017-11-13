using System.Collections.Generic;
using Money.Models;

namespace Money.Db
{
  public interface IDbService
  {
    ICollection<Expense> GetExpenses();
    void AddExpense(Expense expense);
  }
}