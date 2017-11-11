using System.Collections.Generic;
using Money.Models;

namespace Money.StatementParsers
{
  public interface IStatementParser
  {
    bool CanParse(List<string> lines);
    bool TryParse(string line, out Expense expense);
  }
}