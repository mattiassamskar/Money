﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InvoiceParser.Requests;
using MediatR;
using Nancy;

namespace InvoiceParser.Web.Modules
{
  public class UploadModule : NancyModule
  {
    private readonly IMediator _mediator;

    public UploadModule(IMediator mediator) : base("/upload")
    {
      _mediator = mediator;
      Post["/", true] = async (x, ct) =>
      {
        var file = Request.Files.FirstOrDefault();

        if (file == null)
          return Negotiate.WithStatusCode(HttpStatusCode.BadRequest);

        await ParseFile(file.Value);

        return Negotiate.WithStatusCode(HttpStatusCode.OK);
      };
    }

    private async Task ParseFile(Stream stream)
    {
      using (var destinationStream = new MemoryStream())
      {
        stream.CopyTo(destinationStream);
        var text = await _mediator.Send(new ParsePdfRequest { Bytes = destinationStream.ToArray() });
        var expenses = await _mediator.Send(new ParseSkandiaStatementRequest { Text = text });
        expenses.ToList().ForEach(expense => _mediator.Publish(new ExpenseCreatedNotification { Expense = expense }));
      }
    }
  }
}