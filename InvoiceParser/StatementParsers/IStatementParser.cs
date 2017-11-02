using System.Collections.Generic;
using InvoiceParser.Models;

namespace InvoiceParser.StatementParsers
{
  public interface IStatementParser
  {
    bool CanParse(List<string> lines);
    bool TryParse(string line, out Expense expense);
  }
}