using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InvoiceParser.Models;
using InvoiceParser.Requests;
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
      var file = Request.Files.FirstOrDefault();

      if (file == null)
        return Negotiate.WithStatusCode(HttpStatusCode.BadRequest);

      ParseFile(file.Value);

      return Negotiate.WithStatusCode(HttpStatusCode.OK);
    }

    private async void ParseFile(Stream stream)
    {
      using (var destinationStream = new MemoryStream())
      {
        await stream.CopyToAsync(destinationStream);
        var text = await _mediator.Send(new ParsePdfRequest {Bytes = destinationStream.ToArray()});
        var expenses = await _mediator.Send(new ParseSkandiaStatementRequest {Text = text});
        expenses.ToList().ForEach(expense => _mediator.Publish(expense));
      }
    }
  }
}