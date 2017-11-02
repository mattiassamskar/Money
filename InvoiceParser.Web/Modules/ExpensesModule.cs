using System;
using System.Collections.Generic;
using InvoiceParser.Models;
using MediatR;
using Nancy;

namespace InvoiceParser.Web.Modules
{
  public class ExpensesModule : NancyModule
  {
    private readonly IMediator _mediator;

    public ExpensesModule(IMediator mediator) : base("/expenses")
    {
      _mediator = mediator;
      Get["/"] = _ => GetAllExpenses();
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
  }
}