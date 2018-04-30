using System.Collections.Generic;
using Money.Core.Models;

namespace Money.Core.Services
{
  public interface IStatementParser
  {
    bool CanParse(Statement statement);
    IEnumerable<Expense> Parse(Statement statement);
  }
}