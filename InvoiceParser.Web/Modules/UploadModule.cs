using System.IO;
using System.Linq;
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
      Post["/"] = _ => UploadFile();
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
        var text = await _mediator.Send(new ParsePdfRequest { Bytes = destinationStream.ToArray() });
        var expenses = await _mediator.Send(new ParseSkandiaStatementRequest { Text = text });
        expenses.ToList().ForEach(expense => _mediator.Publish(expense));
      }
    }
  }
}