using System.Collections.Generic;
using Money.Models;

namespace Money.StatementParsers
{
  public interface IStatementParser
  {
    bool CanParse(Statement statement);
    IEnumerable<Expense> Parse(Statement statement);
  }
}