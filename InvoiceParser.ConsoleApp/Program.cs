using System;
using System.Collections.Generic;
using System.IO;

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

      if (args.Length != 1)
        Console.WriteLine("Usage: InvoiceParser.ConsoleApp <path to Circle K invoice pdf>");
      else
      {
        if (!File.Exists(args[0]))
        {
          Console.WriteLine($"{args[0]} does not exist");
        }
        else
        {
          var expenses = Expenses.From(new PdfParser().GetTextFromPdf(File.ReadAllBytes(args[0])));

          Console.WriteLine("Total: " + expenses.Sum());

          filters.ForEach(filter =>
          {
            Console.WriteLine(string.Join(", ", filter) + ": " + expenses.ThatMatches(filter).Sum());
          });
        }
      }
    }
  }
}
