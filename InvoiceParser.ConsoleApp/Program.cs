using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InvoiceParser.Requests;

namespace InvoiceParser.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      var filters = new List<List<string>>
      {
        new List<string> {"COOP", "ICA"},
        new List<string> {"NETFLIX"},
        new List<string> {"VIAPLAY"}
      };

      new Runner().Run();
    }
  }

  public class Runner
  {
    public async void Run()
    {
      var mediator = Bootstrap.BuildMediator();

      var filePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\9159-752.337-4_2017-07-26-2017-10-26.pdf";
      var bytes = File.ReadAllBytes(filePath);

      var text = await mediator.Send(new ParsePdfRequest
      {
        Bytes = bytes
      });

      var expenses = await mediator.Send(new ParseSkandiaStatementRequest
      {
        Text = text
      });

      expenses.ToList().ForEach(expense => mediator.Publish(new ExpenseCreatedNotification{Expense = expense}));
    }
  }

  //  return filters.SelectMany(filter => expenses.Where(expense => expense.Description.Contains(filter)));
  //  return expenses.Select(expense => expense.Amount).Sum();
}
