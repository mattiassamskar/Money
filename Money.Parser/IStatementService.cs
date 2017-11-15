using System.Collections.Generic;
using Money.Models;

namespace Money
{
  public interface IStatementService
  {
    IEnumerable<Expense> Parse(byte[] bytes);
  }
}