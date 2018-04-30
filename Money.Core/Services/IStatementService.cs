using System.Collections.Generic;
using Money.Core.Models;

namespace Money.Core.Services
{
  public interface IStatementService
  {
    IEnumerable<Expense> Parse(byte[] bytes);
  }
}