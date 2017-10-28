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

      var filePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\pdf.pdf";
      var bytes = File.ReadAllBytes(filePath);

      var text = await mediator.Send(new ParsePdfRequest
      {
        Bytes = bytes
      });

      var expenses = await mediator.Send(new ParseCirclekInvoiceRequest
      {
        Text = text
      });

      expenses.ToList().ForEach(expense => mediator.Publish(expense));
    }
  }

  //  return filters.SelectMany(filter => expenses.Where(expense => expense.Description.Contains(filter)));
  //  return expenses.Select(expense => expense.Amount).Sum();
}
