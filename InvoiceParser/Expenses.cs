using System.Collections.Generic;
using System.Linq;

namespace InvoiceParser
{
  public class Expenses
  {
    private readonly IEnumerable<Expense> _expenses;
    private List<string> _filters;

    private Expenses(IEnumerable<Expense> expenses)
    {
      _expenses = expenses;
    }

    public static Expenses From(string text)
    {
      return new Expenses(ParseText(text));
    }

    public Expenses ThatMatches(List<string> filters)
    {
      _filters = filters;
      return this;
    }

    public double Sum()
    {
      return GetFilteredTransactions().Select(expense => expense.Amount).Sum();
    }

    private IEnumerable<Expense> GetFilteredTransactions()
    {
      return _filters?.SelectMany(filter => _expenses.Where(expense => expense.Description.Contains(filter))) ?? _expenses;
    }

    private static IEnumerable<Expense> ParseText(string text)
    {
      foreach (var line in GetLinesThatAreExpenses(text.Split('\n')))
      {
        if (Expense.TryParse(line, out var expense))
          yield return expense;
      }
    }

    private static IEnumerable<string> GetLinesThatAreExpenses(IEnumerable<string> lines)
    {
      var withinExpenses = false;

      foreach (var line in lines)
      {
        if (line.StartsWith("MASTERCARD TRANSAKTIONER"))
        {
          withinExpenses = true;
          continue;
        }

        if (line.StartsWith("SUMMA MASTERCARD TRANSAKTIONER"))
        {
          withinExpenses = false;
          continue;
        }

        if (withinExpenses)
          yield return line;
      }
    }
  }
}