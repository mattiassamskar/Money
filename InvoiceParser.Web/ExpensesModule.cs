using System;
using System.Collections.Generic;
using InvoiceParser.Models;
using Nancy;

namespace InvoiceParser.Web
{
  public class ExpensesModule : NancyModule
  {
    public ExpensesModule() : base("/expenses")
    {
      Get["/"] = _ => GetAllExpenses();
      Post["/"] = _ => UploadFile();
    }

    private dynamic GetAllExpenses()
    {
      return new List<Expense>
      {
        new Expense {Date = DateTime.Now, Amount = 100, Description = "Expense 1"},
        new Expense {Date = DateTime.Now, Amount = 200, Description = "Expense 2"},
        new Expense {Date = DateTime.Now, Amount = 300, Description = "Expense 3"}
      };
    }

    private dynamic UploadFile()
    {
      return Negotiate.WithStatusCode(HttpStatusCode.OK);
    }
  }
}