using System.Collections.Generic;
using System.Linq;

namespace InvoiceParser
{
  public class Expenses
  {
    private readonly IEnumerable<Expense> _expenses;

    private Expenses(IEnumerable<Expense> expenses)
    {
      _expenses = expenses;
    }

    public static Expenses From(string text)
    {
      return new Expenses(GetExpensesFromText(text));
    }

    public Expenses ThatMatches(List<string> filters)
    {
      return new Expenses(filters.SelectMany(filter => _expenses.Where(expense => expense.Description.Contains(filter))));
    }

    public double Sum()
    {
      return _expenses.Select(expense => expense.Amount).Sum();
    }

    private static IEnumerable<Expense> GetExpensesFromText(string text)
    {
      var lines = text.Split('\n');
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

        if (withinExpenses && Expense.TryParse(line, out var expense))
          yield return expense;
      }
    }
  }
}